using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2d_controller : MonoBehaviour
{
    public float speed = 50f;
    public float jumpPower = 150f;

    public int jumpCount;
    public bool grounded;

    private Rigidbody _rigi;

    private Vector3 spawn;
    public GameObject blood;


    public Animator anim;
    private AudioSource source;
    public AudioClip run_sound;
    public AudioClip jump_sound;
    public AudioClip hurt_sound;

    public bool buttonIsPressed = false; // for button

  //  public Transform blood;
    // Use this for initialization
    void Start()
    {
        spawn = transform.position;

        _rigi = transform.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();

        blood.GetComponent<ParticleSystem>().enableEmission = false;
        source = GetComponent<AudioSource>();

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
        if (h < 0 || h>0 ) {
            source.PlayOneShot(run_sound);
            anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
            anim.SetBool("Attack", false);

        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 2)
        {
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
            jumpCount++;
            anim.SetBool("Grounded", false);
            source.PlayOneShot(jump_sound);
        }

        if (Input.GetKeyDown(KeyCode.C) && jumpCount == 0) {
            anim.SetBool("Attack", true);
             }

        // GetComponent<MeshRenderer>().flipX = h.x < 0 ? true : false;
    }

    public void OnCollisionEnter(Collision col)
    {

        switch (col.gameObject.tag)
        {
            case "ground":
                anim.SetBool("Grounded", true);
                jumpCount = 0;
                break;
       
            case "enemy":
                if(col.gameObject.transform.position.y- transform.position.y <= -0.2f)
                 {

                    print("I hit enemy");
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

                source.PlayOneShot(hurt_sound);
                blood.GetComponent<ParticleSystem>().enableEmission = true;
                StartCoroutine(stopBlood());

                Debug.Log("Trigger WORKING");
                Instantiate(blood, transform.position, Quaternion.identity);
               transform.position = spawn;
                Debug.Log("spawned");
         

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