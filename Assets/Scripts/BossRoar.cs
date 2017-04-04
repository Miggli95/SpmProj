﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoar : MonoBehaviour {

    private AudioSource source1;
    public AudioClip roarSound;
	// Use this for initialization
	void Start () {
        source1 = GetComponent<AudioSource>();
        source1.clip = roarSound;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (!source1.isPlaying)
        {
            float d = Random.Range(5, 8);
            float p = Random.Range(0.5f, 1.5f);
            source1.pitch = p;
            source1.PlayDelayed(d);
        }
    }
}
