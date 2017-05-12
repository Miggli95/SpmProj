using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLookAt : MonoBehaviour {
    // Use this for initialization
    public Transform child;
    public Transform parent;
    public bool invert;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (other.GetComponent<CharControllerNavMesh>().facingForward)
            other.GetComponent<CharControllerNavMesh>().ResetLookAt();
            /* else
             {
                 other.GetComponent<CharControllerNavMesh>().LookAt(parent);
             }*/
            //Debug.Log("lookAtChild");
        }
    }
}

