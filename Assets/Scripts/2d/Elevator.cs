using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("player"))
        {
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("player"))
        {
            other.transform.parent = null;
        }
    }
}
