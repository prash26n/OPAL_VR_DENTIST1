using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

/* This class holds all excel-file logging functionality */
public class GlobalFunction {
	public static string comma = ",";

    public static void LogToPatientFile(string fileLocation, string sceneName, string description, double time, float anxiety) {
        string row = "\n" + description + comma + time + comma + sceneName + comma + anxiety; 
        System.IO.File.AppendAllText(fileLocation, row);
    }

    public static Dictionary<string, float> sortSceneRankings(Dictionary<string, float> scenes){
    	//this method will update the GlobalVariables.sceneRank list so that it is in order from least anxious --> high anxious
    	var sorted = scenes.OrderBy(x => x.Value);
    	foreach(KeyValuePair<string, float> entry in sorted)
		{
			Debug.Log(entry.Key + ": " + entry.Value);
		    // do something with entry.Value or entry.Key
		}
    	return sorted.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public static void initializeSaveFile(string fileLocation, string patientName, string[] scenes, int index)
    {
    	string row = patientName + "\n";
    	foreach (string i in scenes){
    		row += i + ",";
    	}
    	row = row.Substring(0, row.Length - 1);
    	row += "\n" + index;
 		System.IO.File.WriteAllText(fileLocation, row); 
    }

    public static void generateSceneOrderOnLoad(){

    }
   
}