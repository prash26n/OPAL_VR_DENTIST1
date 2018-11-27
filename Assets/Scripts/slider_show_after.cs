using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_show_after : MonoBehaviour {

    void Start()
    {
        
//        if (gameObject.activeInHierarchy)
//          gameObject.SetActive(false);

       
    }

    // Update is called once per frame
    void Update()
    {

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
