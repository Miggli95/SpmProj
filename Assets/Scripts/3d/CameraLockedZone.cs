using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedZone : MonoBehaviour
{
   public  CameraScript cameraScript;

    public Vector3 rotation;
    public float y;
    public bool lockedRotation = false;
    public bool lockedYPosition = false;
    public bool releaseY = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            if (releaseY)
            {
                cameraScript.ReleaseYPosition();
            }

            if (lockedRotation)
            {
                cameraScript.LockRotation(rotation);
                cameraScript.deadZoneY = 1;
            }

            else if (lockedYPosition)
            {
                cameraScript.LockY(y);
               
            }
        }

       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (lockedRotation)
            {
                cameraScript.ReleaseCamera();
            }


            /*else if (lockedYPosition)
            {
                if (releaseY)
                {
                    cameraScript.ReleaseYPosition();
                }
            }*/

        }
    }
}