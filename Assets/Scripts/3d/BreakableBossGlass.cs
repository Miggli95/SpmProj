using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBossGlass : MonoBehaviour
{

    [SerializeField]
    private GameObject player;


    private BoxCollider boxCol;
    private bool smashed;
    private BoxCollider[] parentColliders;
    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        parentColliders = transform.parent.gameObject.GetComponents<BoxCollider>();

        smashed = false;
    }


    void FixedUpdate()
    {
        if (smashed)
            SmashGlass();
    }



    void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponentInChildren<SphereCollider>())
        {
            Debug.Log("<color=blue>Mechanic:</color> Breakable glass.");
            boxCol.enabled = false;
            smashed = true;
            parentColliders[1].enabled=true;
            player.GetComponent<CharController>().doSortaSuperJump();
        }
    }

    private void SmashGlass()
    {
        Destroy(gameObject);
    }

}