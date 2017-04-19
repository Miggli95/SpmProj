using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewCheckPoint : MonoBehaviour {
    public GameObject checkP2;
    public GameObject checkP3;
    public GameObject checkParticle;
    public GameObject checkP4;
    public GameObject checkP5;
    public GameObject checkP6;
    private player2d_controller playerScript;
    public GameObject Text;
    private AudioSource source;
    public AudioClip soundClip;

    // Use this for initialization
    void Start () {
        playerScript = FindObjectOfType<player2d_controller>();
        source = GetComponent<AudioSource>();
        Text.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator OnTriggerEnter(Collider other)
    {
        
        Destroy(checkParticle);
        Destroy(checkP2);
        Destroy(checkP3);
        Destroy(checkP4);
        Destroy(checkP5);
        Destroy(checkP6);
        playerScript.currentCheckPoint= gameObject;
        Text.SetActive(true);
        if (!source.isPlaying)
        {
                        source.PlayOneShot(soundClip);
        }
        yield return new WaitForSeconds(2f);
        Text.SetActive(false);
        Destroy(soundClip);
        // if (other.name == "player")
        //{
        //   playerScript.currentCheckPoint = gameObject; 
        //}
    }
   
}
