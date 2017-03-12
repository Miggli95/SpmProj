using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public float damping = 1;
    public float rotationSpeed = 2;
    private float rotationX;
    public float min;
    public float max;
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
        float desiredAngleY = target.transform.eulerAngles.y;
        //float currentAngleX = transform.eulerAngles.x;
        //float desiredAngleX = target.transform.eulerAngles.x;

        float angleY = desiredAngleY;// Mathf.LerpAngle(currentAngleY,desiredAngleY,Time.deltaTime*damping);
        //float angleX = Mathf.LerpAngle(currentAngleX,desiredAngleX,Time.deltaTime*damping);
        rotationX -= rotationSpeed * Input.GetAxis("Mouse Y");
        rotationX = Mathf.Clamp(rotationX, min, max);
        Quaternion rotation = Quaternion.Euler(rotationX, angleY, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
    }
}
