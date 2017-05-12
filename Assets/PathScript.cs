using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour {

    // Use this for initialization
    public Transform child;
    public Transform parent;
    //public bool invert;
    void Start()
    {
        if(transform.childCount>0)
        child = transform.GetChild(0);
        parent = transform.parent;

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (other.GetComponent<CharControllerNavMesh>().facingForward)
            other.GetComponent<CharControllerNavMesh>().LookAt(transform);//, invert);
           /* else
            {
                other.GetComponent<CharControllerNavMesh>().LookAt(parent);
            }*/
            //Debug.Log("lookAtChild");
        }
    }
}
