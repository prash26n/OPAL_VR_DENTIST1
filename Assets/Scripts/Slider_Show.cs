using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Show : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void show_trigger()
    {
        Debug.Log("trigger script called");
        //StartCoroutine(ShowAndHide()); // 1 second
        // rend.enabled = true;
        this.gameObject.SetActive(true);
    }

    public void hide()
    {
        Debug.Log("trigger script called");
        this.gameObject.SetActive(false);
    }
}
