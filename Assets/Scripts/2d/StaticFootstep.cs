using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFootstep : MonoBehaviour {
   private AudioSource source;
   public AudioClip clip;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlayStaticFootstepSound() {
        source.PlayOneShot(clip);
    }
}
