﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Saw : MonoBehaviour
{


    // Use this for initialization
    // public GameManager manager;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && other is CapsuleCollider)
        {
            other.gameObject.GetComponent<CharController>().Death();
        }
        if (other.gameObject.CompareTag("player") && other is CapsuleCollider)
        {
            other.gameObject.GetComponent<player2d_controller>().Respwn();
        }
    }
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

    }
}
