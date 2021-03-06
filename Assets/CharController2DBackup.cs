﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController2DBackup : MonoBehaviour
{

    private Vector3 position;
    bool input = false;
    float t;
    CharacterController controller;
    [SerializeField]
    private float stickToGroundForce;
    [SerializeField]
    private float gravityMultiplier;
    Vector3 charinput;
    Vector3 moveDir;
    public float speed;
    private bool jump;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    private bool jumping;
    private bool doubleJump;
    bool previouslyGrounded;
    //public float rotationSpeed;
    private CollisionFlags colFlags;
    public GameManager manager;
    public bool gotKey = false;
    private bool slam = false;
    //float rotationY;
    private SphereCollider aoeSlam;
    private float slamTimer = 0.0f;
    private float flyingTimer = 3.0f;
    private bool flying = false;
    public GameObject slamCollider;
    public GameObject blood;
    public GameObject ShockWave;
    bool lockedRotation;
    public bool buttonIsPressed = false;
    public Animator anim;
    public AudioSource[] clip;
    public AudioClip SlamSound;
    public Vector3 spawn1, spawn2;
    private CountdownTimer countdownTimer;
    public AudioClip jump_sound;
    public AudioClip hurt_sound;
    public AudioClip hurt_sound2;
    public AudioClip flip_sound;
    public GameObject currentCheckPoint;
    float previousX;
    private Rigidbody _rigi;
    float startZ;
    RaycastHit topHit;
    public float BoundeDownOnRoof;
    public float pushPower = 2;

    public GameObject retryMenu;
    public bool retry1 = false;
    public bool retry2 = true;
    // Use this for initialization
    void Start()
    {
        clip = GetComponents<AudioSource>();
        clip[0].clip = SlamSound;
        /*if (SceneManager.GetActiveScene().name == "AlternativeLevel3")
        {
            Death();
        }*/
        startZ = transform.position.z;
        _rigi = transform.GetComponent<Rigidbody>();
        position = transform.position;
        controller = GetComponent<CharacterController>();
        //rotationY = transform.rotation.y;
        aoeSlam = GetComponentInChildren<SphereCollider>();
        anim = gameObject.GetComponent<Animator>();

        // Death();
    }



    /*public void Death()
    {
        print("died");
        transform.position = manager.getSpawnPoint();
        transform.localEulerAngles = manager.getRotation();
    }*/

    public bool isSlaming()
    {
        return slam;
    }

    // Update is called once per frame
    float curMouse = 0;
    float lastMouse = 0;
    private bool airJump;
    private bool jumped;
    private bool bounce;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        charinput = new Vector2(horizontal, 0);

        if (charinput.sqrMagnitude > 1)
        {
            charinput.Normalize();
        }

        if (!jump)
        {
            if (controller.isGrounded)
            {
                jump = Input.GetKeyDown(KeyCode.Space);
            }
        }

        // if (manager.HaveAbility((int)Abilities.doubleJump))
        //{
        if (!airJump && !controller.isGrounded)
        {
            airJump = Input.GetKeyDown(KeyCode.Space);
        }

        if (!previouslyGrounded && controller.isGrounded)
        {
            //moveDir.y = 0;
            jumping = false;
            airJump = false;
            jumped = false;
            //doubleJump = false;
        }

        /*   if (!controller.isGrounded && !jumping && previouslyGrounded)
           {
               moveDir.y = 0;
           }*/

        previouslyGrounded = controller.isGrounded;
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 3)
        {

            manager.ResetProgression();
        }

        RaycastHit rayhit;
        if (Physics.Raycast(transform.position, Vector3.down, out rayhit))
        {

            if (rayhit.collider.tag == "enemy" && rayhit.distance < 0.5f)
            {
                rayhit.collider.GetComponent<EnemyAI2D>().deathAni();
                //moveDir.y = jumpSpeed;
                forceJump();
            }
            if (rayhit.collider.tag == "enemy2" && rayhit.distance < 0.5f)
            {
                rayhit.collider.GetComponent<Enemy2AI3D>().deathAni();
                //_rigi.AddForce(Vector3.up * (jumpSpeed * _rigi.mass * 2f));
                forceJump();
            }
            if (rayhit.collider.tag == "enemy3" && rayhit.distance < 1.4f)
            {
                rayhit.collider.GetComponent<Enemy3AI3D>().deathAni();
                forceJump();
            }
            if (rayhit.collider.tag == "lava" && rayhit.distance < 1.1f)
            {
                //Death();
            }
        }

        if (gotKey)
        {

            Destroy(GameObject.FindWithTag("locker"));
        }
        //}

    }

    void FixedUpdate()
    {
        if (transform.position.z == startZ)
        {
            previousX = transform.position.x;
        }

        t += Time.fixedDeltaTime;
        //slamCollider.SetActive(slam);

        //float vertical = Input.GetAxis("Vertical");


        if (slamTimer > 0.0f)
        {
            slamTimer -= Time.fixedDeltaTime;
            if (slamTimer <= 0.0f)
            {
                aoeSlam.enabled = false;
            }
        }

        /*if (!lockedRotation)
        {
            rotationY = rotationSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, rotationY);
        }*/

        /*
        {
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }*/
        // transform.Rotate(0, charinput.x * rotationSpeed, 0);
        if (Physics.SphereCast(transform.position, controller.radius, Vector3.up, out topHit,
          BoundeDownOnRoof, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            moveDir.y = -1;
        }
        Vector3 destination = transform.right * charinput.x; //Quaternion.Euler(0, transform.rotation.y, 0) * (transform.forward * charinput.y);
        RaycastHit hit;
        //  Ray ray = new Ray(transform.position, Vector3.down);
        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hit,
            controller.height / 2, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;

        moveDir.x = destination.x * speed;
        //moveDir.z = destination.z * speed;




        if (controller.isGrounded)
        {
            moveDir.y = -stickToGroundForce;
            //controller.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 1000, transform.position, ForceMode.Impulse);
            if (jump)
            {
                Debug.Log("jump");
                moveDir.y = jumpSpeed;
                jump = false;
                jumping = true;
            }

            if (slam)
            {
                Vector3 spawnslam = transform.position;
                //spawnslam.y -= 1;
                aoeSlam.enabled = true;
                Instantiate(ShockWave, spawnslam, Quaternion.Euler(90, 0, 0));
                slamTimer = 0.1f;

                clip[0].PlayOneShot(SlamSound);
            }

            slam = false;

        }

        else
        {
            moveDir += Physics.gravity * gravityMultiplier * Time.deltaTime;

            if (manager.HaveAbility((int)Abilities.slam))
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    moveDir.y = -jumpSpeed;
                    slam = true;

                }
            }

            if (airJump)
            {
                if (!jumped)
                {
                    Debug.Log("doubleJump");
                    moveDir.y = doubleJumpSpeed;
                    jumping = false;
                    jump = false;
                    jumped = true;
                }
                //doubleJump = false;
            }
        }
        if (bounce)
        {
            moveDir.y = jumpSpeed;
            bounce = false;
        }




        colFlags = controller.Move(moveDir * Time.fixedDeltaTime);

        if (transform.position.z != startZ)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = previousX;
            newPosition.z = startZ;
            transform.position = newPosition;
        }



        //print("isSlaming" + slam);
        //test code reset progression of gameManager




        if (flying)
        {
            flyingTimer -= Time.fixedDeltaTime;
            if (flyingTimer <= 0.0f)
            {
                loadNextBoss();
            }
        }
    }

    /*public void LockRotation(float rotationY)
    {
        this.rotationY = rotationY;
        lockedRotation = true;
    }*/

    /*public void ReleaseRotation()
    {
        lockedRotation = false;
    }*/

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }

    public void forceJump()
    {
        bounce = true;
        //moveDir.y = jumpSpeed;
    }

    public void doSuperJump()
    {
        flying = true;
        moveDir.y = jumpSpeed * 12;

    }
    public void doSortaSuperJump()
    {
        moveDir.y = jumpSpeed * 2;

    }

    private void loadNextBoss()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "BossLevel":
                SceneManager.LoadScene("BossLevel 2");
                break;
            case "BossLevel 2":
                SceneManager.LoadScene("BossLevel 3");
                break;
            case "BossLevel 3":
                SceneManager.LoadScene("Hub Level");
                break;
        }
    }

    public void loadBoss()
    {
        SceneManager.LoadScene("BossLevel");
    }

    public void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {

            case "movableBox":
                anim.SetBool("Grounded", true);
                anim.SetBool("SecJump", false);
                break;

            case "enemy":
                if (col.gameObject.transform.position.y - transform.position.y <= -0.2f)
                {

                    col.gameObject.GetComponent<EnemyAI2D>().deathAni();
                    // _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
                }
                else
                    col.gameObject.GetComponent<EnemyAI2D>().getDamage();
                break;

            case "locker":
                Debug.Log("Player need a key !!!!!!!");

                break;
        }

    }


    void OnTriggerEnter(Collider col)
    {
        print(col.gameObject);

        if (col.gameObject.CompareTag("leveladvancer"))
        {
            manager.LevelComplete(3);
            SceneManager.LoadScene("Hub Level");
        }

        switch (col.gameObject.tag)
        {
            /*  case "spike":
                  if (SceneManager.GetActiveScene().name != "NyaLevel3")
                  {
                      // Die(spawn1);
                      Respwn();
                      countdownTimer.timer -= 2f;
                  }
                  break;*/



            case "button":                               /// början på kod till button/door switch
                Debug.Log("standing on button");
                if (buttonIsPressed == false)

                {

                    buttonIsPressed = true;
                    Debug.Log("Animation Working");
                    bool b = col.GetComponent<Animation>().Play("ButtonDown");
                    Debug.Log("b =" + b);

                    //   GetComponent<Animation>().Play("DoorOpen");
                }

                break;

            case "levelExit":
                int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
                //if(levelToLoad<=SceneManager.sceneCount)

                SceneManager.LoadScene("Hub Level");
                //countdownTimer.timer = 90f;
                gotKey = false;
                //Application.LoadLevel(SceneManager.);
                manager.LevelComplete(levelToLoad);

                break;

            case "key":
                Debug.Log("standing on button");
                col.gameObject.SetActive(false);
                gotKey = true;
                break;

            case "Level1":
                // if (Input.GetKey(KeyCode.UpArrow))
                if (manager.isLevelComplete(0) && manager.level1Cleared)
                {
                    retry1 = true;
                    retryMenu.SetActive(true);

                }
                else if (manager.isLevelComplete(0))
                {
                    SceneManager.LoadScene("level1");
                }
                break;

            case "Level2":
                if (manager.isLevelComplete(1) && manager.level2Cleared)
                {
                    retry2 = true;
                    retryMenu.SetActive(true);

                }
                else if (manager.isLevelComplete(1))// && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SceneManager.LoadScene("Level2");
                }
                break;

            case "Level3":
                if (manager.isLevelComplete(2)) //&& Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SceneManager.LoadScene("NyaLevel3");
                }
                break;

            case "bossLevel":
                if (manager.isLevelComplete(3))// && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SceneManager.LoadScene("BossLevel");
                }
                break;



        }


    }

    public void Die(Vector3 spawn)
    {
        Instantiate(blood, transform.position, Quaternion.identity); // spelar upp blood på den "spike" du träffar
        transform.position = spawn;   // spawn
    }

    public void Respwn()
    {
        Debug.Log("respawn");
        clip[1].PlayOneShot(hurt_sound);
        clip[1].PlayOneShot(hurt_sound2);
        Instantiate(blood, transform.position, Quaternion.identity);
        Vector3 spawnPoint = currentCheckPoint.transform.position; ;
        spawnPoint.z = startZ;
        transform.position = spawnPoint;
        moveDir = Vector3.zero;
    }

    IEnumerator stopBlood()
    {
        yield return new WaitForSeconds(1f);
        blood.GetComponent<ParticleSystem>().enableEmission = false;

    }

}