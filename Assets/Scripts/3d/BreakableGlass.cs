using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGlass : MonoBehaviour
{

    public GameObject Player;
    private MeshCollider Meshcol;

    private void Start()
    {
        Meshcol = GetComponent<MeshCollider>();
    }

    
   


    
    void OnTriggerEnter(Collider other)
    {
        if (other == Player.GetComponent<SphereCollider>())
        {
            Debug.Log("triggad");

            Meshcol.enabled = false;

        }
    }

}