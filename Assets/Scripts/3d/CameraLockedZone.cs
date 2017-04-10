using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedZone : MonoBehaviour
{
   public  CameraScript cameraScript;

    public Vector3 rotation;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraScript.LockRotation(rotation);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraScript.ReleaseCamera();
        }
    }
}