using System.Collections;
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
            Debug.Log(d);
            source1.PlayDelayed(d);
        }
    }
}
