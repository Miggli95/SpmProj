/****************************************
 * NullReferenceException: Object reference not set to an instance of an object
 * 
 * -resolved-
 * **************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSign : MonoBehaviour
{
	[SerializeField] private GameObject textObject;
	[SerializeField] private AudioClip soundClip;

	private AudioSource source;
	private bool alreadyPlay;

    // Use this for initialization
    void Start()
    {
		textObject.SetActive(false);
		try{
        source = GetComponent<AudioSource>();
		}catch(UnityException e){
			Debug.Log ("<color=red>TextSign Error: </color>No audio attatched to text sign.");
		}
		alreadyPlay = false;
    }

    // Update is called once per frame
	void Update(){}

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



    void ObjOn()
    {
		textObject.SetActive(true);
    }

    void ObjOff()
    {
		textObject.SetActive(false);
    }
}
