using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreBoardScript : MonoBehaviour {
    public playerCam camera;
    public Vector3 offset;
    private Vector3 originalOffset;
	// Use this for initialization
	void Start () {
        originalOffset = camera.offset;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            camera.offset = offset;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            camera.offset = originalOffset;
        }
    }


}
