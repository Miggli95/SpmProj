﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theHat : MonoBehaviour
{

    [SerializeField]
    private GameObject player;


    private BoxCollider boxCol;
    private bool smashed;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();


        smashed = false;
    }


    void FixedUpdate()
    {
        if (smashed)
            SmashGlass();
    }



    void OnTriggerEnter(Collider other)
    {
       
        if (other == player.GetComponentInChildren<SphereCollider>() && this.transform.position.y <= player.transform.position.y+2)
        {
            Debug.Log("<color=blue>Mechanic:</color> Breakable glass.");
            boxCol.enabled = false;
            smashed = true;
            player.GetComponent<CharControllerNavMesh>().doSuperJump();
        }
    }

   /* void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            player.GetComponent<CharControllerNavMesh>().AgentOn(false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            player.GetComponent<CharControllerNavMesh>().AgentOn(true);
    }*/

    

    private void SmashGlass()
    {
            Destroy(gameObject);
    }

}