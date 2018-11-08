using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public static class GlobalVariables
{

    //Initialize array of scenes here:
   public static string[] scenes = {"1 Reception", "2 X Ray Room", "3 Dentist Room", "5. temp"};

    //strPath holds path to user's desktop
    private static string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

    //filename of patient's shuffled .csv
    private static string filename = strPath + "\\Patient_Default_Shuffled_OPAL_Testing.csv";

    //filename of patient's save .txt
    public static string savefilename = strPath + "\\default_save.txt";
    
    //patient name
    private static string patientname;

    //Sample rate for records in patient file
    private static float sampleRate = 2.0f; 

    
    //How long anxiety level needs to be below threshold before transitioning to next scene.
    private static int timeTillNextScene = 5;

    //scene instruction hash map. Keys are scene names, values are instructions to display.
    public static Dictionary<string, string> SceneInstructions = new Dictionary<string, string>
        {
            { "1 Reception", "You are in the reception at the Dental Clinic." },
            { "2 X Ray Room", "You have reached the X Ray Room before the examination. Feel free to approach the chair and gauge your anxiety level." },
            { "3 Dentist Room", "The dentist is examining a patient. Rate your anxiety level." },
            { "5. temp", "Rate your anxiety level." }
        };

    //global flag that represents whether or not the user has finished Tutorial.cs
    public static bool tutorialDone = false;

    //global flag that represents patient has existing save
    public static bool isExisting = false;

    //array that holds scene order for existing user
    public static string[] existingSceneOrder = new string[scenes.Length];

    //slider threshold to proceed to next scene for existing users
    public static float existingThreshold = 15.0f;

    //exact time when user enters first scene of experience
    public static double startTime = 0;

    //list that ranks scenes in order of least anxious -> most anxious by final anxiety level
    public static Dictionary<string, float> scenesRank = new Dictionary<string, float>();

    //value of slider
    public static float sliderValue = 0f;

    
    //call GlobalVariables.Filename to return path+filename
    public static string Filename 
    {
        get 
        {
            return filename;
        }
        set 
        {
            filename = value;
        }
    }

    //call GlobalVariables.Patientname to return patient's full name.
    public static string Patientname 
    {
        get 
        {
            return patientname;
        }
        set 
        {
            patientname = value;
        }
    }

    //call GlobalVariables.Scenes to return array of scenes
    public static string[] Scenes 
    {
        get 
        {
            return scenes;
        }
        set 
        {
            scenes = value;
        }
    }

    //Call GlobalVariables.SampleRate to return sampleRate
    public static float SampleRate 
    {
        get 
        {
            return sampleRate;
        }
        set 
        {
            sampleRate = value;
        }
    }

    public static int TimeTillNextScene
    {
        get 
        {
            return timeTillNextScene;
        }
        set 
        {
            timeTillNextScene = value;
        }
    }

}