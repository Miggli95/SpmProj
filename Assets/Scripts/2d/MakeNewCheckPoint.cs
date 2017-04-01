using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewCheckPoint : MonoBehaviour {
    public GameObject checkP2;
    public GameObject checkP3;
    public GameObject checkParticle;
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
        source.PlayOneShot(soundClip);
        Destroy(checkParticle);
        Destroy(checkP2);
        Destroy(checkP3);
        playerScript.currentCheckPoint= gameObject;
        Text.SetActive(true);

        yield return new WaitForSeconds(2f);
        Text.SetActive(false);
        Destroy(soundClip);
        // if (other.name == "player")
        //{
        //   playerScript.currentCheckPoint = gameObject; 
        //}
    }
   
}
