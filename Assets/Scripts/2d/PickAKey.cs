using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAKey : MonoBehaviour {
    public GameObject panal;
  //  public GameObject gotKeyParicle;
	// Use this for initialization
	void Start () {
        panal.SetActive(false);
     //   gotKeyParicle.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "player") {
          //  Vector3 spawnGetKey = transform.position;
          //  gotKeyParicle.transform.position = spawnGetKey;
         //   gotKeyParicle.SetActive(true);
            gameObject.SetActive(false);
            panal.SetActive(true);
        }

        }
    }

