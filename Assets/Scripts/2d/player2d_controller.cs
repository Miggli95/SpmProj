using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player2d_controller : MonoBehaviour
{
    public float speed = 50f;
    public float jumpPower = 150f;
   
   // public bool canDoubleJump;
    public bool gotKey =  false;
    private Rigidbody _rigi;

    private Vector3 spawn1,spawn2;
    public GameObject blood;


    public Animator anim;
    private AudioSource source;
    
    public AudioClip jump_sound;
    public AudioClip hurt_sound;
    public AudioClip hurt_sound2;
    public AudioClip flip_sound;
    public GameObject currentCheckPoint;
    public bool buttonIsPressed = false; // for button
    private bool facingRight;
    private CountdownTimer countdownTimer;
    
    public bool stopjump;
    float t;
    private float slamTimer = 0.0f;
    private SphereCollider aoeSlam;
    private bool slam = false;
    //public ParticleSystem pe ;
    // Use this for initialization
    public AudioClip slam_sound;
    public GameObject slame;
    public GameManager gm;
    public GameObject walkingDust;
    void Start()
    {

     
        spawn1 = transform.position;

        _rigi = transform.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();

        //   blood.GetComponent<ParticleSystem>().enableEmission = false;
        source = GetComponent<AudioSource>();
        facingRight= true;
        
        stopjump = false;
        aoeSlam = GetComponentInChildren<SphereCollider>();
        //pe = gameObject.GetComponent<ParticleSystem>();
        walkingDust.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        stopSlam();


       
        /*if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 1)
        {
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
            jumpCount--;
            OnGround = false;
            anim.SetBool("Grounded", false);
            source.PlayOneShot(jump_sound);

        }*/
    }
    void Update()
    {
        t += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("kkkkkkkkk");
            gm.ResetProgression();
        }
        //moving the player

        float move = Input.GetAxis("Horizontal");
        _rigi.AddForce((Vector2.right * speed) * move);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("Attack", false);
        if (move > 0 && !facingRight)
        {
            Flip();
           // walkingDust.SetActive(true);
            // pe.Play();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
            //walkingDust.SetActive(true);
            // pe.Play();
        }

         if (move == 0||!IsGrounded()) {
             // pe.Stop();
        walkingDust.SetActive(false);
            } else walkingDust.SetActive(true);

        IsGrounded();
        

        /* RaycastHit hit;
         Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider>().center;
         Debug.DrawRay(physicsCentre, Vector3.down * 0.415f, Color.red, 1);
         if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 0.415f))
         {
             if (hit.transform.gameObject.tag != "player")
             {
                 falljump = false;
             }
         }
         else
         {
             falljump = true;
         }
         //Debug.Log(onGround);

 */
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigi.Sleep();
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
            
            // canDoubleJump = true;
            source.PlayOneShot(jump_sound);
            anim.SetBool("Grounded", false);
        }
         if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && stopjump ==false )
        {
            
            _rigi.Sleep();
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
         //   canDoubleJump = false;
            stopjump = true;
            anim.SetBool("SecJump", true);
            source.PlayOneShot(jump_sound);
            
            //Debug.Log(fallCount);
        } 
        

      


        if (gotKey)
        {

            Destroy(GameObject.FindWithTag("locker"));
        }
        if (slamTimer > 0.0f)
        {
            slamTimer -= Time.deltaTime;
            if (slamTimer <= 0.0f)
            {
                aoeSlam.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.V) && !IsGrounded())
        {
            anim.SetBool("Attack", true);
            _rigi.AddForce(Vector3.down * (jumpPower * _rigi.mass * 2f));
            slam = true;
        }
            if (IsGrounded())
        {
            if (slam)
            {
                aoeSlam.enabled = true;
                Instantiate(slame, transform.position, Quaternion.Euler(90, 0, 0));
                slamTimer = 0.1f;

                source.PlayOneShot(slam_sound);
                
            }
            _rigi.AddForce(Vector3.down * 0);
            slam = false;

        }
        RaycastHit rayhit;
        if (Physics.Raycast(transform.position, Vector3.down, out rayhit))
        {

            if (rayhit.collider.tag == "enemy" && rayhit.distance < 1.4f)
            {
                rayhit.collider.GetComponent<EnemyAI3D>().deathAni();
                //forceJump();
            }
            if (rayhit.collider.tag == "enemy2" && rayhit.distance < 1.3f)
            {
                rayhit.collider.GetComponent<Enemy2AI3D>().deathAni();
                _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
                //forceJump();
            }
            if (rayhit.collider.tag == "enemy3" && rayhit.distance < 1.4f)
            {
                rayhit.collider.GetComponent<Enemy3AI3D>().deathAni();
                //forceJump();
            }
            if (rayhit.collider.tag == "lava" && rayhit.distance < 1.1f)
            {
                //Death();
            }
        }


    }
    private bool IsGrounded()
    {
        Vector3 physicCentre = this.GetComponent<CapsuleCollider>().bounds.center;
        Vector3 leftRayStart;
        Vector3 rightRayStart;
        RaycastHit hit;
        leftRayStart = this.GetComponent<CapsuleCollider>().bounds.center;
        rightRayStart= this.GetComponent<CapsuleCollider>().bounds.center;
        leftRayStart.x -= this.GetComponent<CapsuleCollider>().bounds.extents.x;
        rightRayStart.x += this.GetComponent<CapsuleCollider>().bounds.extents.x;

        Debug.DrawRay(leftRayStart, Vector3.down * 0.415f, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down * 0.415f, Color.green);
        if (Physics.Raycast(leftRayStart, Vector3.down, out hit, 0.415f))
        {
            
            return true;
        }
        if (Physics.Raycast(rightRayStart, Vector3.down, out hit, 0.415f))
        {
            
            return true;
        }
        
        return false;
    }

    void Flip() {

        source.PlayOneShot(flip_sound);
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }
    public void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {

            case "movableBox":
            anim.SetBool("Grounded", true);
                anim.SetBool("SecJump",false);
                break;

            case "ground":
                anim.SetBool("Grounded", true);
                anim.SetBool("SecJump", false);
                stopjump = true;
                break;

            case "enemy":
                if (col.gameObject.transform.position.y - transform.position.y <= -0.2f)
                {

                    col.gameObject.GetComponent<EnemyAI2D>().deathAni();
                    _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
                }
               else
                   col.gameObject.GetComponent<EnemyAI2D>().getDamage();
                break;

            case "locker" :
                Debug.Log("Player need a key !!!!!!!");
                
                break;
        }
        
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "ground"|| col.gameObject.tag == "movableBox")

            stopjump = false;

    }

    void OnTriggerEnter(Collider col)
    {
        print(col.gameObject);
        switch (col.gameObject.tag)
        {
          

            case "spike":
                if (SceneManager.GetActiveScene().name != "NyaLevel3")
                {
                    
                    // Die(spawn1);
                    Respwn();
                    countdownTimer.timer -= 2f;
                }
                break;

          

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
                    
                SceneManager.LoadScene(4);
                //countdownTimer.timer = 90f;
                gotKey = false;
                //Application.LoadLevel(SceneManager.);
                gm.LevelComplete(levelToLoad);
                
                break;

                    case     "key":
                       Debug.Log("standing on button");
                        col.gameObject.SetActive(false);
                         gotKey= true;
                break;


        }

        
    }

    public void OnTriggerStay(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Level1":
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    SceneManager.LoadScene("level1");
                }
                break;

            case "Level2":
                if (gm.isLevelComplete(1) && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SceneManager.LoadScene("Level2");
                }
                break;
            case "Level3":
                if (gm.isLevelComplete(2) && Input.GetKeyDown(KeyCode.UpArrow))
                {
                   SceneManager.LoadScene("AlternativeLevel3");
                }
              
                break;

            case "bossLevel":
                if (gm.isLevelComplete(3) && Input.GetKeyDown(KeyCode.UpArrow))
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
        source.PlayOneShot(hurt_sound);
        source.PlayOneShot(hurt_sound2);
        Instantiate(blood, transform.position, Quaternion.identity);
        _rigi.transform.position =currentCheckPoint.transform.position;
    }

    IEnumerator stopBlood()
    {
        yield return new WaitForSeconds(1f);
        blood.GetComponent<ParticleSystem>().enableEmission = false;

    }
    IEnumerator stopSlam()
    {
        yield return new WaitForSeconds(1f);
        slame.GetComponent<ParticleSystem>().enableEmission = false;

    }

}