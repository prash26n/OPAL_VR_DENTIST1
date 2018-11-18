using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappearAfterSeconds : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Hide(10.0f));
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Hide(float delay)
    {
        //this.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}
