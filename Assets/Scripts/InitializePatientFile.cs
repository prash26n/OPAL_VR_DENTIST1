using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;



public class InitializePatientFile : MonoBehaviour {

	public InputField iField;
	public string patientName;
	public string patientFile;

	public void StartPatientFile(){
		patientName = iField.text;
		if(patientName.Trim() != ""){
			patientName = patientName.Trim().Replace(" ", "_");
			string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + "\\OPAL_Testing";
	 		patientFile = strPath + "\\Patient_" + patientName.ToLower() + "_Shuffled_OPAL_Testing.csv";
	 		System.IO.Directory.CreateDirectory(strPath);
	 		GlobalVariables.Filename = patientFile;
	 		GlobalVariables.savefilename = strPath + "\\" + patientName.ToLower() + "_save.txt";
	 		GlobalVariables.Patientname = patientName;
	 		System.IO.File.WriteAllText(patientFile, 
	 									"Patient's Name:,"+patientName+",,\n" + ",,,\n" + "Description,Time,Scene,Anxiety Level");
			SceneManager.LoadScene("Welcome");
		}
	}
}
