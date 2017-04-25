using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewCheckPoint : MonoBehaviour
{
    public GameObject checkP2;
    public GameObject checkP3;
    public GameObject checkParticle;
    public GameObject checkP4;
    public GameObject checkP5;
    public GameObject checkP6;
    private CharController2D playerScript;
    public GameObject Text;
    private AudioSource source;
    public AudioClip soundClip;
    private bool alreadyPlay;



    // Use this for initialization
    void Start()
    {
        playerScript = FindObjectOfType<CharController2D>();
        source = GetComponent<AudioSource>();
        Text.SetActive(false);
        checkParticle.SetActive(true);
        checkP2.SetActive(false);
        checkP3.SetActive(true);
        checkP4.SetActive(false);
        checkP5.SetActive(false);
        checkP6.SetActive(false);
        alreadyPlay = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator OnTriggerEnter(Collider other)
    {

        checkP2.SetActive(true);

        checkP4.SetActive(true);
        checkP5.SetActive(true);
        checkP6.SetActive(true);
        //   Destroy(checkParticle);
        //   Destroy(checkP2);
        //   Destroy(checkP3);
        //   Destroy(checkP4);
        //   Destroy(checkP5);
        //  Destroy(checkP6);
        playerScript.currentCheckPoint = gameObject;
        Text.SetActive(true);

        if (!alreadyPlay)
        {
            source.PlayOneShot(soundClip);
            alreadyPlay = true;
        }
        yield return new WaitForSeconds(1f);
        Text.SetActive(false);
        Destroy(soundClip);
        yield return new WaitForSeconds(2f);
        Destroy(checkP4);
        Destroy(checkP5);
        Destroy(checkP6);
        // if (other.name == "player")
        //{
        //   playerScript.currentCheckPoint = gameObject; 
        //}
    }

}
