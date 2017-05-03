///*******************************
// * NullReferenceException: Object reference not set to an instance of an object
// * DisplayUI.Start () (at Assets/Scripts/2d/DisplayUI.cs:15)
// *
// * -sesolved- 
// * *******************************/
//
//
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//
//public class DisplayUI : MonoBehaviour {
//    public string textToDisplay;
//    public Text targetTextDisplayObject;
//    public float fadeTime;
//    public bool displayInfo;
//   
//    // Use this for initialization
//    void Start () {
//        targetTextDisplayObject = GameObject.Find("CollisionText").GetComponent<Text>();
//        targetTextDisplayObject.color = Color.clear;	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//
//    void FixedUpdate()
//    {
//        FadeText();
//    }
//
//    void OnCollisionEnter(Collision col)
//    {
//        if (col.collider.CompareTag("player"))
//        {
//            displayInfo = true;
//        }
//    }
//    void OnCollisionExit(Collision col)
//    {
//        if (col.collider.CompareTag("player"))
//        {
//            displayInfo = false;
//        }
//    }
//
//    void FadeText() {
//        if (displayInfo)
//        {
//            targetTextDisplayObject.text = textToDisplay;
//            targetTextDisplayObject.GetComponent<Text>().enabled = true;
//           // myText.color = Color.Lerp(myText.color, Color.red, fadeTime * Time.deltaTime);
//        }
//        else if (!displayInfo) {
//            targetTextDisplayObject.GetComponent<Text>().enabled = false;
//           // myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
//        }
//
//    }
//}
//


using UnityEngine;

[RequireComponent( typeof(BoxCollider), typeof(BoxCollider))]
public class DisplayUI : MonoBehaviour {
	[SerializeField] private GameObject textDisplayObject;



	void Start(){
		if (textDisplayObject == null) {
			throw new UnityException ("<color=red>Exit Text Error: </color>Text is null.");
		}

		textDisplayObject.SetActive (false);
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("player")) {
			textDisplayObject.SetActive (true);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.CompareTag ("player")) {
			textDisplayObject.SetActive (false);
		}
	}
		
}