using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScript : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject caveCamera;

    // Use this for initialization
    void Start () {
        caveCamera.SetActive(false);

    }

    // Update is called once per frame
    void Update() {

    }


 void OnTriggerEnter(Collider other)
    {
        mainCamera.SetActive(false);
        caveCamera.SetActive(true);
    }
 void OnTriggerExit(Collider other)
    {
        caveCamera.SetActive(false);
        mainCamera.SetActive(true);
    }
}
