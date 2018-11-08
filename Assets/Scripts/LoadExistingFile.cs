using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class LoadExistingFile : MonoBehaviour {

	public InputField iField;
	public string patientName;
	public string patientFile;
	public Text error;

	public void loadFile(){
		patientName = iField.text.Trim().Replace(" ", "_").ToLower();
		Debug.Log(patientName);
		if(patientName.Trim() != "")
		{
			string path = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + "\\OPAL_Testing\\"+patientName+"_save.txt";
			if(System.IO.File.Exists(path)){
				Debug.Log("File exists!");
				//implement logic to initialize scene order here
				GlobalVariables.isExisting = true;
				System.IO.StreamReader st = new System.IO.StreamReader(path);
				st.ReadLine();  // read and discard the first line
				string scenes = st.ReadLine().ToString();
				st.Close();
				//Debug.Log(scenes);
				char[] splitchar = { ',' };
				GlobalVariables.existingSceneOrder = scenes.Split(splitchar);
				//initialize sceneorder array
				SceneManager.LoadScene("Tutorial");
			}
			else{
				error.text = "Error: Patient file not found. Please make sure the patient's save file is in the OPAL_Testing directory on the desktop, and try again.";
			}
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
