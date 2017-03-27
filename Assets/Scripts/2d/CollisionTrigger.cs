using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    [SerializeField]
    public BoxCollider platformCollider;
    [SerializeField]
    public BoxCollider platformTrigger;
    // Use this for initialization
    void Start()
    {
        playerCollider = GameObject.FindWithTag("player").GetComponent<CapsuleCollider>();
        Physics.IgnoreCollision(platformCollider, platformTrigger, true);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player")
        {
            Physics.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }
 void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "player")
        {
            Physics.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }

    
}
