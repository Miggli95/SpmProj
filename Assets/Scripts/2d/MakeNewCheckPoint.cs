using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewCheckPoint : MonoBehaviour {
    public GameObject checkP2;
    public GameObject checkP3;
    public GameObject checkParticle;
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

        Destroy(checkParticle);
        Destroy(checkP2);
        Destroy(checkP3);
        playerScript.currentCheckPoint= gameObject;


       // if (other.name == "player")
        //{
         //   playerScript.currentCheckPoint = gameObject; 
        //}
    }
}
