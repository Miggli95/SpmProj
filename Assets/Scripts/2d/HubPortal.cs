using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPortal : MonoBehaviour {
    public GameManager gm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        switch (this.gameObject.tag)
        {
            case "Level1":
                if (gm.isLevelComplete(0))
                {
                    GetComponent<Collider>().isTrigger = true;
                    GetComponent<Renderer>().material.color = Color.yellow;
                }
                else
                {
                    GetComponent<Collider>().isTrigger = false;
                    GetComponent<Renderer>().material.color = Color.black;
                }
                break;
            case "Level2":
                if (gm.isLevelComplete(1))
                {
                    GetComponent<Collider>().isTrigger = true;
                    GetComponent<Renderer>().material.color = Color.yellow;
                }
                else
                {
                    GetComponent<Collider>().isTrigger = false;
                    GetComponent<Renderer>().material.color = Color.black;
                }
                break;
            case "Level3":
                if (gm.isLevelComplete(2))
                {
                    GetComponent<Collider>().isTrigger = true;
                    GetComponent<Renderer>().material.color = Color.yellow;
                }
                else
                {
                    GetComponent<Renderer>().material.color = Color.black;
                    GetComponent<Collider>().isTrigger = false;
                }
                break;
            case "bossLevel":
                if (gm.isLevelComplete(3))
                {
                    GetComponent<Collider>().isTrigger = true;
                    GetComponent<Renderer>().material.color = Color.yellow;
                }
                else
                {
                    GetComponent<Collider>().isTrigger = false;
                    GetComponent<Renderer>().material.color = Color.black;
                }
                break;



        }


    }
}
