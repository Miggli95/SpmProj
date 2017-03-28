using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayUI : MonoBehaviour {
    public string myString;
    public Text myText;
    public float fadeTime;
    public bool displayInfo;
   
    // Use this for initialization
    void Start () {
        myText = GameObject.Find("CollisionText").GetComponent<Text>();
        myText.color = Color.clear;
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        FadeText();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("player"))
        {
            displayInfo = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.CompareTag("player"))
        {
            displayInfo = false;
        }
    }

    void FadeText() {
        if (displayInfo)
        {
            myText.text = myString;
            myText.GetComponent<Text>().enabled = true;
           // myText.color = Color.Lerp(myText.color, Color.red, fadeTime * Time.deltaTime);
        }
        else if (!displayInfo) {
            myText.GetComponent<Text>().enabled = false;
           // myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
        }

    }
}
