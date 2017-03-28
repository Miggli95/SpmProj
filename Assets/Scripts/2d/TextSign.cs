using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSign : MonoBehaviour {
    public GameObject CollText1;

	// Use this for initialization
	void Start () {
        CollText1.SetActive(false);
       
        
    }
	
	// Update is called once per frame
	void Update () {
       



    }
    IEnumerator OnTriggerEnter(Collider other) {
        if (other.CompareTag("player")) {
            TextOn();
            yield return new WaitForSeconds(3f);
            TextOff();
        }
        
    }

    

    public void TextOn() {
        CollText1.SetActive(true);
    }

    public void TextOff() {
        CollText1.SetActive(false);
    }
}
