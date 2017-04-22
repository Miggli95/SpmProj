using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLockscr : MonoBehaviour {

	// Use this for initialization
	void Start () {





		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectsWithTag("key").Length == 0)
        {
            Destroy(this.gameObject);
        } 
		
	}
}
