using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewCheckPoint : MonoBehaviour {

    public player2d_controller playerScript;

    // Use this for initialization
    void Start () {
        playerScript = FindObjectOfType<player2d_controller>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {

        playerScript.currentCheckPoint= gameObject;
       // if (other.name == "player")
        //{
         //   playerScript.currentCheckPoint = gameObject;
        //}
    }
}
