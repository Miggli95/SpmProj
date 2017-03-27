using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit3D : MonoBehaviour {

	[SerializeField] private GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other == player.GetComponent<SphereCollider>()) {
			Debug.Log("<color=blue>Mechanic:</color> Load Boss Level.");
			Application.LoadLevel("BossLevel");
		}
	}

}
