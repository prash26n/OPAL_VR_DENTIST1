using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        
            //load the scene
            SceneManager.LoadScene("1 Reception");

        

    }
}
