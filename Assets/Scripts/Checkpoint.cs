using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour 
{

    // Use this for initialization
    public int checkpoint = 0;
    public GameManager manager;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Checkpoint");
            manager.setRespawn(checkpoint);
        }
    }
}
