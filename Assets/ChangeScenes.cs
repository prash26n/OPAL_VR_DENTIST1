using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{

    public string scene;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //function to change scenes
    //replace SceneName with name of scene you want to change to

    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}