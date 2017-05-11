using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharControllerNavMesh : MonoBehaviour
{ 
    private Vector3 position;
    bool input = false;
    float t;
    bool airJump = false;
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
    public float rotationSpeed;
    private CollisionFlags colFlags;
    public GameManager manager;
    private bool slam = false;
    float rotationY;
    private SphereCollider aoeSlam;
    private float slamTimer = 0.0f;
    private float flyingTimer = 3.0f;
    private bool flying = false;
    public GameObject slamCollider;
    private Animator anim;
    public GameObject ShockWave;
    bool lockedRotation;
    public Transform CharRefTransform;

    public AudioSource[] clip;
    public AudioClip SlamSound;
    public Vector3 spawn1, spawn2;
    public float slamEffectTimer;
    public ParticleSystem slamParticle;
    bool startSlam = false;
    public GameObject finalBossScream;
    public bool facingForward = true;
    public CountTime countTime;
    public ScoreCount DeathCount;
    public float totalTime;
    // Use this for initialization
    public Transform lookAt;
    NavMeshAgent agent;
    public bool invert;
    public bool grounded = false;
    public bool agentOn = true;
    void Awake()
    {
        lookAt = transform;
        agent = GetComponent<NavMeshAgent>();
        if (SceneManager.GetActiveScene().name == "BossLevel 2" || SceneManager.GetActiveScene().name == "BossLevel 3")
        {
            countTime.timer = manager.getBossTime();
            Debug.Log("bossTime" + manager.getBossTime());
        }
    }

    public void AgentOn(bool on)
    {
        agentOn = on;
        //agent.enabled = on;
    }

    void Start()
    {
        clip = GetComponents<AudioSource>();
        clip[0].clip = SlamSound;

        if (SceneManager.GetActiveScene().name == "AlternativeLevel3")
        {
            Death();
        }

        position = transform.position;
        controller = GetComponent<CharacterController>();
        rotationY = transform.rotation.y;
        aoeSlam = GetComponentInChildren<SphereCollider>();
        anim = GetComponent<Animator>();
        ShockWave.SetActive(false);
        slamEffectTimer = slamParticle.main.duration - 0.1f;

        if (SceneManager.GetActiveScene().name == "level1")
        {
            totalTime = 0;
        }

        else
        {
            totalTime = manager.getCurrentTime();
        }
        AgentOn(true);
        // Death();
    }

    void Flip()
    {
        //source.PlayOneShot(flip_sound);
        facingForward = !facingForward;
        Vector3 theScale = CharRefTransform.localScale;
        theScale.z *= -1;
        CharRefTransform.localScale = theScale;
    }

    public void LookAt(Transform target, bool invert)
    {
        lookAt = target;
        this.invert = invert;
    }

    public void Death()
    {
        print("died");

        transform.position = manager.getSpawnPoint();
        transform.localEulerAngles = manager.getRotation();
        DeathCount.AddScore();
        agent.enabled = false;
        AgentOn(false);
        dead = true;
        colFlags = controller.Move(new Vector3(0,1,0));
        //grounded = controller.isGrounded;
    }

    public bool isSlaming()
    {
        return slam;
    }
    // Update is called once per frame
    float curMouse = 0;
    float lastMouse = 0;
    private bool jumped;
    private bool bounce;
    bool dead = false;
    Quaternion rotation;
    Vector3 lookPos;
    void Update()
    {
        agentOn = agent.enabled;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(vertical));
        charinput = new Vector2(horizontal, vertical);
        grounded = controller.isGrounded;

        if (charinput.sqrMagnitude > 1)
        {
            charinput.Normalize();
        }

        if (dead)
        {
            if (controller.isGrounded)
            {
                AgentOn(true);
                dead = false;
            }
        }
       
        
      

        if (!jump)
        {
            if (controller.isGrounded)
            {
                jump = Input.GetKeyDown(KeyCode.Space);
                //Debug.Log("jump" + Input.GetKeyDown(KeyCode.Space));
            }
        }
        if (vertical > 0 && !facingForward)
        {
            Flip();
            // walkingDust.SetActive(true);
            // pe.Play();
        }
        else if (vertical < 0 && facingForward)
        {
            Flip();
            //walkingDust.SetActive(true);
            // pe.Play();
        }

        // if (manager.HaveAbility((int)Abilities.doubleJump))
        //{
        if (!airJump && !controller.isGrounded)
        {
            airJump = Input.GetKeyDown(KeyCode.Space);
        }

        if (manager.HaveAbility((int)Abilities.slam))
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                startSlam = true;
            }
        }

        if (!previouslyGrounded && controller.isGrounded)
        {
            //moveDir.y = 0;
            jumping = false;
            airJump = false;
            jumped = false;
            //doubleJump = false;
        }

        /*if (!controller.isGrounded && !jumping && previouslyGrounded)
        {
            moveDir.y = 0;
        }*/

        previouslyGrounded = controller.isGrounded;
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 4)
        {

            manager.ResetProgression();
        }
        Vector3 capsuleTarget = transform.position;
        capsuleTarget.y -= 1f;
        RaycastHit rayhit;
        if (Physics.CapsuleCast(transform.position, capsuleTarget, controller.radius, Vector3.down, out rayhit))
        {

            if (rayhit.collider.tag == "enemy" && rayhit.distance < 0.3f)
            {
                print(rayhit.distance);
                rayhit.collider.GetComponent<EnemyAI3D>().deathAni();
                // moveDir.y = jumpSpeed;
                forceJump();
            }
            if (rayhit.collider.tag == "enemy2" && rayhit.distance < 0.4f)
            {
                rayhit.collider.GetComponent<Enemy2AI3D>().deathAni();
                //_rigi.AddForce(Vector3.up * (jumpSpeed * _rigi.mass * 2f));
                forceJump();
            }
            if (rayhit.collider.tag == "enemy3" && rayhit.distance < 0.5f)
            {
                rayhit.collider.GetComponent<Enemy3AI3D>().deathAni();
                forceJump();
            }

        }
        if (Physics.Raycast(transform.position, Vector3.down, out rayhit))
        {
            if (rayhit.collider.tag == "lava" && rayhit.distance < 1.2f)
            {
                Death();
            }
            if (rayhit.collider.tag == "spike" && rayhit.distance < 1.3f)
            {
                Death();
            }
        }
        //}

        anim.SetBool("Grounded", controller.isGrounded);
        // anim.SetBool("Jump", jump);
        anim.SetBool("SecJump", airJump);
        anim.SetBool("Attack", slam);
    }

    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        //slamCollider.SetActive(slam);

        if (slamTimer > 0.0f)
        {
            slamTimer -= Time.fixedDeltaTime;
            if (slamTimer <= 0.0f)
            {
                aoeSlam.enabled = false;
            }
        }

        if (slamEffectTimer > 0.0)
        {
            slamEffectTimer -= Time.fixedDeltaTime;

        }
        else
        {
            ShockWave.SetActive(false);
        }

        if (!lockedRotation)
        {
           // rotationY = rotationSpeed * Input.GetAxis("Mouse X");
           //transform.Rotate(Vector3.up, rotationY);
        }

        /*
        {
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }*/
        // transform.Rotate(0, charinput.x * rotationSpeed, 0);
        /* if (charinput.y < 0)
         {
             charinput.y = 0;
         }*/

        if (facingForward)
        {
            if (lookAt.childCount == 0)
            {
                lookPos = lookAt.position - this.transform.position;
                lookPos.y = 0;
                rotation = Quaternion.LookRotation(lookPos);
            }
            else
            {
                lookPos = lookAt.GetChild(0).position - this.transform.position;
                lookPos.y = 0;
                rotation = Quaternion.LookRotation(lookPos);
            }
            
        }
        else
        {
            int invertInt;
            if (invert)
            {
                invertInt = 1;
            }

            else
            {
                invertInt = -1;
            }
            lookPos = lookAt.parent.position - this.transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos * invertInt);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        Vector3 destination = transform.forward * charinput.y + transform.right * charinput.x;
        if (charinput.x == 0 && charinput.y == 0)
        {
            destination = Vector3.zero;
            //   moveDir.x = 0;
            //  moveDir.z = 0;
        }

        // Vector3 destination = transform.right * charinput.x + Quaternion.Euler(0, transform.rotation.y, 0) * (transform.forward * charinput.y);
        RaycastHit hit;
        //  Ray ray = new Ray(transform.position, Vector3.down);
        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hit,
            controller.height / 2, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;

        moveDir.x = destination.x * speed;
        moveDir.z = destination.z * speed;




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
                spawnslam.y -= 1;
                aoeSlam.enabled = true;
                ShockWave.transform.position = spawnslam;
                ShockWave.SetActive(true);
                slamParticle.Clear();
                slamParticle.Play();
                //     Instantiate(ShockWave, spawnslam, Quaternion.Euler(90, 0, 0));
                slamTimer = 0.1f;
                slamEffectTimer = slamParticle.main.duration - 0.1f;

                clip[0].PlayOneShot(SlamSound);
            }

            slam = false;

        }

        else
        {
            moveDir += Physics.gravity * gravityMultiplier * Time.deltaTime;

            if (startSlam)
            {
                moveDir.y = -jumpSpeed;
                slam = true;
                startSlam = false;

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

        if (flying)
        {
            if (bounce)
            {
                moveDir.y = jumpSpeed * 12;
                bounce = false;
            }
            flyingTimer -= Time.fixedDeltaTime;
            if (flyingTimer <= 0.0f)
            {
                if (SceneManager.GetActiveScene().name != "BossLevel 3")
                {
                    manager.SetBossTime(countTime.timer);
                }
                loadNextBoss();
            }
        }
        else
        {
            if (bounce)
            {
                moveDir.y = jumpSpeed;
                bounce = false;
            }
        }


        if (controller.isGrounded)
        {
            if (moveDir.y > 0)
            {
                agent.enabled = false;
                colFlags = controller.Move(new Vector3(0, moveDir.y, 0) * Time.fixedDeltaTime);
            }

            else
            {
                agent.enabled = true;
                agent.Move(new Vector3(moveDir.x, 0, moveDir.z) * Time.fixedDeltaTime);
            }
           

        }

        else
        {
            agent.enabled = false;
            colFlags = controller.Move(moveDir* Time.fixedDeltaTime);
        }




        //print("isSlaming" + slam);
        //test code reset progression of gameManager

    }

    public void LockRotation(float rotationY)
    {
        this.rotationY = rotationY;
        lockedRotation = true;
    }

    public void ReleaseRotation()
    {
        lockedRotation = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //dont move the rigidbody if the character is on top of it

        if (colFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(controller.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

    public void forceJump()
    {
        bounce = true;
        //moveDir.y = jumpSpeed;
    }
    public void doSuperJump()
    {
        if (SceneManager.GetActiveScene().name == "BossLevel 3")
        {
            finalBossScream.GetComponent<MyOtherSingleton>().playVoice();
        }
        flying = true;
        bounce = true;

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
                manager.setCurrentTime(totalTime + countTime.timer);
                // manager.SetBossTime(countTime.timer);
                SceneManager.LoadScene("BossLevel 2");
                break;
            case "BossLevel 2":
                //manager.SetBossTime(countTime.timer);
                manager.setCurrentTime(totalTime + countTime.timer);
                SceneManager.LoadScene("BossLevel 3");
                break;
            case "BossLevel 3":
                manager.AddYourScore(countTime.timer);
                manager.AddBestScore(countTime.timer);
                manager.setCurrentTime(totalTime + countTime.timer);
                manager.setTotalTime(totalTime + countTime.timer);
                manager.setBestTotalTime(totalTime + countTime.timer);
                SceneManager.LoadScene("EndCredit");
                break;
        }
    }
    public void loadBoss()
    {
        SceneManager.LoadScene("BossLevel");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "leveladvancer")
        {
            manager.LevelComplete(3);
            SceneManager.LoadScene("Hub Level");
        }
    }

}



