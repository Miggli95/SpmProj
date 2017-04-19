using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour {

    public GameObject player;      

    public Vector3 offset;
    Vector3 to;
    public Vector2 deadZone;
    private Vector3 velocity;
    public float time;
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        if(offset == null)
        offset = transform.position - player.transform.position;
    }

    
    void LateUpdate()
    {
       velocity = Vector3.zero;
       Vector3 targetPos = player.transform.position;
      
        if (Mathf.Abs(transform.position.x - targetPos.x) > deadZone.x)
        {
            to.x = targetPos.x;
        }


        if (Mathf.Abs(transform.position.y - targetPos.y) > deadZone.y)
        {
            to.y = targetPos.y;
        }



        transform.position = Vector3.SmoothDamp(transform.position, to + offset,ref velocity, time);
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
    }
}

