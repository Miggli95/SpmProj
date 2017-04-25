using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSign : MonoBehaviour
{
    public GameObject Obj;
    private AudioSource source;
    public AudioClip soundClip;
    private bool alreadyPlay;

    // Use this for initialization
    void Start()
    {
        Obj.SetActive(false);
        source = GetComponent<AudioSource>();
        alreadyPlay = false;


    }

    // Update is called once per frame
    void Update()
    {




    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {

            ObjOn();

            if (!alreadyPlay)
            {
                source.PlayOneShot(soundClip);
                alreadyPlay = true;
            }
            yield return new WaitForSeconds(3f);
            ObjOff();
        }

    }



    public void ObjOn()
    {
        Obj.SetActive(true);
    }

    public void ObjOff()
    {
        Obj.SetActive(false);
    }
}
