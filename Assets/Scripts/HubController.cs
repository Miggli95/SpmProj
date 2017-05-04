using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour {
	[SerializeField] private GameManager gameManger;
	[SerializeField] private int levelToLoad;

	[SerializeField] private GameObject block;
	[SerializeField] private GameObject portal;

	private TitleEventManger titleEventManger;

	// Use this for initialization
	void Start () {
		if (block == null || portal == null)
			Debug.Log ("<color=red> Hub Controller Error: </color>Block or portal missing.");

		if (gameManger.isLevelComplete (levelToLoad)) {
			block.SetActive (false);
			portal.SetActive (true);
		} else {
			block.SetActive (true);
			portal.SetActive (false);
		}

		GameObject tem = GameObject.Find ("TitleEventManger");
		titleEventManger = (TitleEventManger)tem.GetComponent (typeof(TitleEventManger));
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			titleEventManger.ShowCorrectBG (levelToLoad);
		}
	}
}
