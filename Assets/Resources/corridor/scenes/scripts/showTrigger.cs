using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTrigger : MonoBehaviour {

    // Use this for initialization
    public Renderer rend;
    void Start () {
        //StartCoroutine(hide());
        //rend = GetComponent<Renderer>();
        //rend.enabled = false;
        Debug.Log("trigger script  START called");
        //this.gameObject.SetActive(false);
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
        this.gameObject.SetActive(false);
    }

}
