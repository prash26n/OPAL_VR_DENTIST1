using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;


/* Although this class is vaguely called DisplayTitle, it really handles the timing logic for progressing through scenes in a new user's first go at OPAL */

public class DisplayTitle : MonoBehaviour {

	public GameObject SceneTitleCanvas;
	public GameObject AnxietyReminder;
	public GameObject player;
	public Text title;
	public Text instruction;
	public Scene scene;
	public int sceneCount;
	bool pressedTrigger = false;
	bool ratedInitialAnxiety = false;
	bool invoked = false;
	bool invokedb = false;
	bool prompted = false;
	bool flag = false;
	Vector3 pos;
	static public string[] copyOfScenesArray = new string[GlobalVariables.Scenes.Length];

	public void closeCanvas(){
		SceneTitleCanvas.SetActive(false);
	}


	// Use this for initialization
	void Start () {
		//not existing patient
		if(!GlobalVariables.isExisting){
			//fetch the current scene
			scene = SceneManager.GetActiveScene();
			sceneCount = Array.IndexOf(copyOfScenesArray, scene.name) + 1;
            //fetch the instruction text from GlobalVariables.cs
            title.text = GlobalVariables.SceneInstructions[scene.name];
			//set instruction text
			instruction.text = "Please <b>rate your initial anxiety</b> level now.";
			instruction.fontSize = 12;
			//fetch the player's position
			pos = player.transform.position;
		}
		//existing patient
		else{
			scene = SceneManager.GetActiveScene();
			sceneCount = Array.IndexOf(GlobalVariables.existingSceneOrder, scene.name) + 1;
			title.text = GlobalVariables.SceneInstructions[scene.name];
			instruction.text = "<b>Press either Trigger to begin.</b>";
			instruction.fontSize = 16;

		}
	}
	
	public void RateAtTenSeconds(){
		//log anxiety value when prompt to rate anxiety appears Math.Floor(Time.time - GlobalVariables.startTime)
		GlobalFunction.LogToPatientFile(GlobalVariables.Filename, scene.name, "At 10 Seconds (Rate Your Anxiety Prompt)", Math.Floor(Time.time - GlobalVariables.startTime), GlobalVariables.sliderValue);
		prompted = true;
		AnxietyReminder.SetActive(true);
		
	}

	public void changeFlags(){
		ratedInitialAnxiety = true;
		instruction.text = "<b>Press either Trigger to begin.</b>";
		instruction.fontSize = 16;
	}

	public string[] orderByHierarchy(string[] scenes){
		string lowestscene = scenes[0];
		string[] f = new string[GlobalVariables.Scenes.Length];
		char temp = lowestscene[0];
		int count = 0;
		if(Char.ToLower(temp).Equals('c')){
			//grab all cheese and put at start of array, then apple
			foreach (string i in scenes){
				if(Char.ToLower(i[0]).Equals('c')){
					f[count] = i;
					count++;
				}
			}
			foreach (string i in scenes){
				if(Char.ToLower(i[0]).Equals('a')){
					f[count] = i;
					count++;
				}
			}
		}
		else{
			//grab all apple and put at start of array, then cheese
			foreach (string i in scenes){
				if(Char.ToLower(i[0]).Equals('a')){
					f[count] = i;
					count++;
				}
			}
			foreach (string i in scenes){
				if(Char.ToLower(i[0]).Equals('c')){
					f[count] = i;
					count++;
				}
			}
		}
		return f;
	}

	public void LogAndTransition(){
		//log to file
		GlobalFunction.LogToPatientFile(GlobalVariables.Filename, scene.name, "Final", Math.Floor(Time.time - GlobalVariables.startTime), GlobalVariables.sliderValue);
		GlobalVariables.scenesRank.Add(scene.name,GlobalVariables.sliderValue);
		if(sceneCount < 0 || sceneCount >= copyOfScenesArray.Length)
		{
			//analysis of scene results, generate order for future loading
			GlobalVariables.scenesRank = GlobalFunction.sortSceneRankings(GlobalVariables.scenesRank);
			string[] finalsceneorder = GlobalVariables.scenesRank.Keys.ToArray();
			finalsceneorder = orderByHierarchy(finalsceneorder);
			GlobalFunction.initializeSaveFile(GlobalVariables.savefilename, GlobalVariables.Patientname, finalsceneorder,0);
			//go to goodbye scene
			SceneManager.LoadScene("Goodbye");
		}
		else
		{

			SceneManager.LoadScene(copyOfScenesArray[sceneCount]);
		}
	}

	public void proceedToNextScene(){
		if(GlobalVariables.sliderValue <= GlobalVariables.existingThreshold){
			if(sceneCount < 0 || sceneCount >= GlobalVariables.existingSceneOrder.Length)
			{
				SceneManager.LoadScene("Goodbye");
			}
			else
			{

				SceneManager.LoadScene(GlobalVariables.existingSceneOrder[sceneCount]);
			}
		}
	}

	void Update () {		
		if(scene.name != "Tutorial"){
			if(!GlobalVariables.isExisting){

				if((OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey("2") || Input.GetKey("1")) && !invoked){
					Invoke("changeFlags", 2);
					invoked = true;
				}
				//then, if ratedInitialAnxiety & the trigger buttons are pressed, mark pressedTrigger as true
				if((OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey("9")) && ratedInitialAnxiety && !pressedTrigger){
					pressedTrigger = true;
					GlobalFunction.LogToPatientFile(GlobalVariables.Filename, scene.name, "Initial", Math.Floor(Time.time - GlobalVariables.startTime), GlobalVariables.sliderValue);
				}
				
				//if !pressedtrigger and !ratedinitialanxiety, keep the player at the current position
				if(!(pressedTrigger && ratedInitialAnxiety))
				{
					player.transform.position = pos;
				}
				//else, closecanvas.
				else{
					closeCanvas();
					if(!invokedb){
						Invoke("RateAtTenSeconds",10);
						invokedb = true;
					}
					if(invokedb && prompted && !flag){
						if(OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey("2") || Input.GetKey("1")){
							AnxietyReminder.SetActive(false);
							Invoke("LogAndTransition",15);
							flag = true;
						}
					}
					return;
				}
			}
			else{
				if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey("9")){
					closeCanvas();
				}
				if(GlobalVariables.sliderValue <= GlobalVariables.existingThreshold){
					Invoke("proceedToNextScene",10);
				}
			}
		}
		else
		{
			if(GlobalVariables.tutorialDone){
				//not existing patient
				if(!GlobalVariables.isExisting){
					GlobalVariables.startTime = Math.Floor(Time.time);
					SceneManager.LoadScene(copyOfScenesArray[sceneCount]);
				}
				else{
					GlobalVariables.startTime = Math.Floor(Time.time);
					SceneManager.LoadScene(GlobalVariables.existingSceneOrder[sceneCount]);
				}
			}
		}
	}
}
