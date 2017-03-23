using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAKey : MonoBehaviour {
    public GameObject panal;
	// Use this for initialization
	void Start () {
        panal.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "player") {
            panal.SetActive(true);
        }

        }
    }

