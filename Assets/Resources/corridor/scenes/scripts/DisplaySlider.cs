using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySlider : MonoBehaviour {

    // Use this for initialization
    showTrigger triggerObject;

    void Start () {
        triggerObject = GameObject.Find("Trigger").GetComponent<showTrigger>();
        triggerObject.hide();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {   
        
        
        triggerObject.show_trigger();
    }



}
