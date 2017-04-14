using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private BoxCollider[] Colliders;
    // Use this for initialization
    void Start () {
		
	}
	public void fillHole (){
        transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        Colliders = GetComponents<BoxCollider>();
        Colliders[1].enabled = false;
        Destroy(transform.GetChild(1).gameObject);
    }
    public void spawnEnemies()
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
