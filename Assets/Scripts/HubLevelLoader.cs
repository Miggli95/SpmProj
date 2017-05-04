using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubLevelLoader : MonoBehaviour {

	[SerializeField] private string levelNameToLoad;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("player") || col.CompareTag ("Player")) {
			Debug.Log ("collider");
			SceneManager.LoadScene (levelNameToLoad,LoadSceneMode.Single);
		}
	}
}
