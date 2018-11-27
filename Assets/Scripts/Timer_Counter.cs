using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Counter : MonoBehaviour {

    // Use this for initialization
    Slider_Show sliderObject;
    Slider_Show infoObject;
    void Start()
    {
      sliderObject = GameObject.Find("Trigger").GetComponent<Slider_Show>();
        infoObject = GameObject.Find("info_after_trigger_appear").GetComponent<Slider_Show>();
        sliderObject.hide();
        infoObject.hide();
        StartCoroutine(LateCall());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(14.0f);
        Debug.Log("After wait for time here");
        sliderObject.show_trigger();
        infoObject.show_trigger();
        StartCoroutine(AnotherLateCall());
        //Do Function here...
    }
    IEnumerator AnotherLateCall()
    {

        yield return new WaitForSeconds(4.0f);
       
        infoObject.hide();
       //Do Function here...
    }
}
