using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlide : MonoBehaviour {

    public Animator anim;

    public void openDoor()
    {
        Debug.Log("Open door");
        anim.SetBool("Open", true);

    }
    public void closeDoor() {
        anim.SetBool("Open", false);

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
