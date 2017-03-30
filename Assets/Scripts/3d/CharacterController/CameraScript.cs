using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public float damping = 1;
    public float rotationSpeed = 2;
    private float rotationX;
    private float angleY;
    public float min;
    public float max;
    private float rotationYOffset = 0;
    Vector3 offset;


    // Use this for initialization
    void Start()
    {
        offset = target.transform.position - transform.position;
        rotationX = transform.rotation.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float currentAngleY = transform.eulerAngles.y;
        float desiredAngleY = target.transform.eulerAngles.y + rotationYOffset;
        //float currentAngleX = transform.eulerAngles.x;
        //float desiredAngleX = target.transform.eulerAngles.x;

        angleY = desiredAngleY;// Mathf.LerpAngle(currentAngleY,desiredAngleY,Time.deltaTime*damping);
        //float angleX = Mathf.LerpAngle(currentAngleX,desiredAngleX,Time.deltaTime*damping);
        rotationX -= rotationSpeed * Input.GetAxis("Mouse Y");
        rotationX = Mathf.Clamp(rotationX, min, max);
        Quaternion rotation = Quaternion.Euler(rotationX, angleY, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        if (!hit.transform.gameObject.CompareTag("Player"))
        {

            transform.position = transform.position - transform.forward*2;
                //offset.z--;
               // offset.y+=1/7f;
            
        }
    }
}
