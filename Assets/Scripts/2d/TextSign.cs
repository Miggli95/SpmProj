using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSign : MonoBehaviour {
    public GameObject CollText1, Triggertext2;

	// Use this for initialization
	void Start () {
        CollText1.SetActive(false);
        Triggertext2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator OnTriggerEnter(Collider other) {
        if (other.CompareTag("player")) {
            Triggertext2.SetActive(true);
            yield return new WaitForSeconds(3f);
            Triggertext2.SetActive(false);
        }
        
    }

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.name == "player")
        {
            CollText1.SetActive(true);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "player")
        {
            CollText1.SetActive(false);
        }
    }
    
}
