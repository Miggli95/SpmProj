using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public int z;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, z) * Time.deltaTime);
    }
}
