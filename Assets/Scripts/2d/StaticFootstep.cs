using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFootstep : MonoBehaviour {
   private AudioSource[] sources;
    private AudioSource sources1;
    private AudioSource sources2;
    public AudioClip clip;
    public AudioClip clip2;
    // Use this for initialization
    void Start () {
        sources = GetComponents<AudioSource>();
        sources1 = sources[0];
        sources2 = sources[1];
        AnimationEvent ae = new AnimationEvent();
        ae.messageOptions = SendMessageOptions.DontRequireReceiver;
    }
	
	// Update is called once per frame
	void Update () {
        if (sources2.isPlaying)
            sources2.Stop();
    }
    void PlayStaticFootstepSound()
    {
        if (sources1.isPlaying)
        {
            sources2.Stop();
        }
        if (!sources2.isPlaying)
        {
            sources2.PlayOneShot(clip);
        }
    }
    void PlayStaticFootstepSound2()
    {
        if (sources1.isPlaying)
        {
            sources2.Stop();
        }

        if (!sources2.isPlaying)
        {
            sources2.PlayOneShot(clip2);
        }
    }
    void Stop() {
        sources2.Stop();

    }
}
