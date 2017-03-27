using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGlass : MonoBehaviour
{

	[SerializeField] private GameObject player;
	[SerializeField] private float fallSpeed;
	[SerializeField] private float breakTimer;

    private MeshCollider meshCol;
	private bool smashed;

    private void Start() {
        meshCol = GetComponent<MeshCollider>();

		if (fallSpeed == 0||breakTimer == 0)
			throw new UnityException ("<color=red>Error: Breakable glass falling speed or break timer cannot be equal to zero.</color>");

		smashed = false;
    }

    
	void FixedUpdate(){
		if (smashed)
			SmashGlass ();
	}


    
    void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<SphereCollider>()) {
			Debug.Log("<color=blue>Mechanic:</color> Breakable glass.");
            meshCol.enabled = false;
			smashed = true;
        }
    }

	private void SmashGlass(){
		breakTimer -= Time.deltaTime;
		if (breakTimer > 0)
			transform.Translate (Vector3.down * Time.deltaTime * fallSpeed, Space.World);
		else
			Destroy (gameObject);
	}

}