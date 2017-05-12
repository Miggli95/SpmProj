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
    public Vector3 offset;
    bool lockedRotation = false;
    Quaternion rotation;
    float positionY;
    bool lockedYPosition = false;
    public float deadZoneY = 5;
    private float VelY;
    // Use this for initialization
    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = target.transform.position - transform.position;
        }

        rotationX = transform.rotation.x;
    }

    public void LockRotation(Vector3 rotation)
    {
        this.rotation = Quaternion.Euler(rotation);
        lockedRotation = true;
        target.GetComponent<CharController>().LockRotation(rotation.y);
    }

    public void ReleaseCamera()
    {
        lockedRotation = false;
        target.GetComponent<CharController>().ReleaseRotation();
    }

    public void LockY(float y)
    {
        lockedYPosition = true;
        positionY = y;
    }

    public void ReleaseYPosition()
    {
        lockedYPosition = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log("lockedRotation" + lockedRotation);
        float currentAngleY = transform.eulerAngles.y;
        float desiredAngleY = target.transform.eulerAngles.y + rotationYOffset;
        //float currentAngleX = transform.eulerAngles.x;
        //float desiredAngleX = target.transform.eulerAngles.x;

        angleY = desiredAngleY;// Mathf.LerpAngle(currentAngleY,desiredAngleY,Time.deltaTime*damping);
        //float angleX = Mathf.LerpAngle(currentAngleX,desiredAngleX,Time.deltaTime*damping);
        rotationX -= rotationSpeed * Input.GetAxis("Mouse Y");
        rotationX = Mathf.Clamp(rotationX, min, max);

        if (!lockedRotation)
        {
            rotation = Quaternion.Euler(rotationX, angleY, 0);
        }

        /*if (!lockedYPosition)
        {
            transform.position = target.transform.position - (rotation * offset);
        }

        else
        {
            Vector3 targetPos = target.transform.position;
            transform.position = new Vector3(targetPos.x, positionY, targetPos.z) - (rotation * offset);
        }*/
       
        Vector3 targetPos = target.transform.position;

        if (Mathf.Abs(transform.position.y - targetPos.y) > deadZoneY)
        {
            positionY = Mathf.SmoothDamp(positionY,targetPos.y,ref VelY,1);
        }

        transform.position = new Vector3(targetPos.x, positionY, targetPos.z) - (rotation * offset);
        transform.LookAt(target.transform);

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        /*if (!hit.transform.gameObject.CompareTag("Player"))
        {

            transform.position = transform.position - transform.forward*2;
                //offset.z--;
               // offset.y+=1/7f;
            
        }*/
    }
}
