﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2d_controller : MonoBehaviour
{
    public float speed = 50f;
    public float jumpPower = 150f;

    public int jumpCount;
    public bool grounded;
    private Rigidbody _rigi;

    public Animator anim;
    public AudioClip run_sound;
    public AudioClip jump_sound;
    public AudioClip hurt_sound;

    public bool buttonIsPressed = false; // for button

    public Transform blood;
    // Use this for initialization
    void Start()
    {
        _rigi = transform.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        blood.GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        //moving the player
        _rigi.AddForce((Vector2.right * speed) * h);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 2)
        {
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
            jumpCount++;
            anim.SetBool("Grounded", false);
        }
        // GetComponent<MeshRenderer>().flipX = h.x < 0 ? true : false;
    }

    public void OnCollisionEnter(Collision col)
    {

        switch (col.gameObject.tag)
        {
            case "ground":
                Debug.Log("123");
                anim.SetBool("Grounded", true);
                jumpCount = 0;
                break;
            case "spike":
                Debug.Log("Dead");
                blood.GetComponent<ParticleSystem>().enableEmission = true;
                StartCoroutine(stopBlood());
                break;
            case "enemy":
                if(col.gameObject.transform.position.y- transform.position.y <= -0.2f)
                 {
                    col.gameObject.GetComponent<EnemyAI2D>().deathAni();
                    _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
                }
                else
                    Debug.Log(col.gameObject.GetComponent<EnemyAI2D>().getDamage());
                break;
        }

    }

    

    void OnTriggerEnter(Collider col)
    {

        switch (col.gameObject.tag)
        {

            case "spike":
                Debug.Log("Trigger WORK HE DIED");
                Application.LoadLevel(0);
                break;

            case "button":                               /// början på kod till button/door switch
                Debug.Log("standing on button");
                if(buttonIsPressed == false)
                    
                {
                   

                        buttonIsPressed = true;
                        Debug.Log("Animation Working");
                        bool b = col.GetComponent<Animation>().Play("ButtonDown");
                        Debug.Log("b =" + b);
                    
                 //   GetComponent<Animation>().Play("DoorOpen");
                }


                break;

        }

    }

    IEnumerator stopBlood()
    {
        yield return new WaitForSeconds(1f);
        blood.GetComponent<ParticleSystem>().enableEmission = false;

    }

}