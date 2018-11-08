using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class ToggleSlider : MonoBehaviour {

	/* Global variables needed for Tutorial scene */

	public GameObject tutorialCanvas1;
	public GameObject tutorialCanvas2;
	public GameObject tutorialCanvas3;
	double belowThresholdTimeTutorial;
	bool belowThresholdTutorial	= true;
	static public bool stepOne = true;
	static public bool stepTwo = false;
	static public bool TutorialDone = false;
	static public bool done = false;
	public Rigidbody tutorialSphere;
	public int threshold = 20;

	/* Global variables for the anxiety slider */

	//anxiety slider canvas
	public GameObject canvas;

	//anxiety slider text
	public Text sliderText;

	//the anxiety slider
	public Slider slider;

	//is the anxiety canvas active?
	bool isActive;

	//max value of slider
	public int maxValue = 100;

	//min value of slider
	public int minValue = 0;

	//default value of slider
	int val = 50;

	//following time variables used for making the slider disappear after a couple seconds. There's a better way of doing this
	double startingtime;
	double heldTimeA;
	double heldTimeB;
	double belowThresholdTime;
	bool belowThreshold = false;

	public Scene scene;



	//note to future self: Learn about C#'s Time.time
	public static double ConvertToUnixTimestamp(DateTime date){
	    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	    TimeSpan diff = date.ToUniversalTime() - origin;
	    return Math.Floor(diff.TotalSeconds);
	}

	public void toggleAndIncrement(){
		startingtime = ConvertToUnixTimestamp(DateTime.Now);
		canvas.SetActive(true);
		if(int.Parse(sliderText.text) < maxValue){
			val = int.Parse(sliderText.text) + 1;
		}
		else{
			val = maxValue;
		}
		sliderText.text = val.ToString();
        slider.value = val;
		isActive = true;
	}
	public void toggleAndDecrement(){
		startingtime = ConvertToUnixTimestamp(DateTime.Now);
		canvas.SetActive(true);
		if(int.Parse(sliderText.text) > minValue){
			val = int.Parse(sliderText.text) - 1;
		}
		else{
			val = minValue;
		}
		sliderText.text = val.ToString();
        slider.value = val;
		isActive = true;
	}


	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene();		
		slider.value = 50;
		belowThresholdTutorial	= true;
	}

	// Update is called once per frame
	void Update () {
		GlobalVariables.sliderValue = slider.value;
		//keyboard shortcuts to manipulate anxiety slider
		if(Input.GetKey("1")){
			toggleAndIncrement();
		}
		if(Input.GetKey("2")){
			toggleAndDecrement();
		}

		//keyboard shortcut to skip tutorial
		if(Input.GetKey("9") && scene.name == "0 Tutorial"){
            SceneManager.LoadScene(1);
        }

		/* 1. Handles Oculus touch input and incrementing/decrementing anxiety value */
		if(OVRInput.GetDown(OVRInput.Button.One)){
			heldTimeA = ConvertToUnixTimestamp(DateTime.Now);
			toggleAndIncrement();
		}
		else if(OVRInput.Get(OVRInput.Button.One)){
			if(ConvertToUnixTimestamp(DateTime.Now) - heldTimeA >= .75){
				toggleAndIncrement();
			}
		}
		if(OVRInput.GetDown(OVRInput.Button.Two)){
			heldTimeB = ConvertToUnixTimestamp(DateTime.Now);
			toggleAndDecrement();
		}
		else if(OVRInput.Get(OVRInput.Button.Two)){
			if(ConvertToUnixTimestamp(DateTime.Now) - heldTimeB >= .75){
				toggleAndDecrement();
			}
		}
		/* 2. Handles disabling canvas if it's active */
		if(isActive && ConvertToUnixTimestamp(DateTime.Now) - startingtime >= 5){
        	//canvas.SetActive(false);
        	isActive = false;
		}


		/* 3. Following code is solely used for tutorial scene, should move it out of Update() and to a tutorial-specific script */
		if(stepOne){
			if(val < 100){
				belowThresholdTutorial = true;
			}
			if(val >= 100 && belowThresholdTutorial){
				belowThresholdTimeTutorial = ConvertToUnixTimestamp(DateTime.Now);
				belowThresholdTutorial	= false;
			}
			if(ConvertToUnixTimestamp(DateTime.Now) - belowThresholdTimeTutorial >= 2 && !belowThresholdTutorial){
				tutorialCanvas1.SetActive(false);
				tutorialCanvas2.SetActive(true);
				tutorialCanvas3.SetActive(false);
				stepTwo	= true;
				stepOne = false;
				TutorialDone = false;
			}
		}
		else if(stepTwo){
			if(tutorialSphere.isKinematic){
				tutorialCanvas1.SetActive(false);
				tutorialCanvas2.SetActive(false);
				tutorialCanvas3.SetActive(true);
				stepTwo	= false;
				stepOne = false;
				TutorialDone = true;
			}
		}
		if(TutorialDone){
			if(val <= 0){
				GlobalVariables.tutorialDone = true;
			}
		}

	}

}
