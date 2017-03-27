using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {


    // Use this for initialization

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player")&& col == col.GetComponent<CapsuleCollider>())
        {
            col.gameObject.GetComponent<CharController>().Death(col.gameObject.GetComponent<CharController>().spawn1);
            
        }
    }
    void Start () {



		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
