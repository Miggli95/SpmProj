using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Saw : MonoBehaviour
{


    // Use this for initialization
    // public GameManager manager;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharController>().Death();
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
