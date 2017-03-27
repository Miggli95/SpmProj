using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player2d_controller : MonoBehaviour
{
    public float speed = 50f;
    public float jumpPower = 150f;

    
   
    public bool canDoubleJump;
    public bool gotKey =  false;
    private Rigidbody _rigi;

    private Vector3 spawn1,spawn2;
    public GameObject blood;


    public Animator anim;
    private AudioSource source;
    
    public AudioClip jump_sound;
    public AudioClip hurt_sound;
    public AudioClip flip_sound;
    private Vector3 currentSpawn;
    public bool buttonIsPressed = false; // for button
    private bool facingRight;
    private CountdownTimer countdownTimer;
    public ParticleSystem pe ;
    // Use this for initialization
    public GameObject Exitsign;
    void Start()
    {

        //  spawn1 = new Vector3(-3, 2, 0);           // första spawnen, spawn1 = transform.position för att komma åt där karaktären är.
        //  spawn2 = new Vector3(5, -11, 0);
        spawn1 = transform.position;

        _rigi = transform.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();

        //   blood.GetComponent<ParticleSystem>().enableEmission = false;
        source = GetComponent<AudioSource>();
        facingRight= true;

        pe = gameObject.GetComponent<ParticleSystem>();
        Exitsign.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        


       
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
     

    //moving the player

    float move = Input.GetAxis("Horizontal");
        _rigi.AddForce((Vector2.right * speed) * move);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("Attack", false);
        if (move > 0 && !facingRight)
        {
            Flip();
            pe.Play();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
            pe.Play();
        }

        if (move == 0) {
            pe.Stop();

        }

        IsGrounded();
       /* RaycastHit hit;
        Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider>().center;
        Debug.DrawRay(physicsCentre, Vector3.down * 0.415f, Color.red, 1);
        if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 0.415f))
        {
            if (hit.transform.gameObject.tag != "player")
            {
                onGround = true;
            }
        }
        else
        {
            onGround = false;
        }
        //Debug.Log(onGround);
*/
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
            canDoubleJump = true;
            source.PlayOneShot(jump_sound);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && canDoubleJump)
        {
            _rigi.Sleep();
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 2f));
            canDoubleJump = false;
            source.PlayOneShot(jump_sound);
        }

        if (gotKey)
        {

            Destroy(GameObject.FindWithTag("locker"));
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
          
            break;

            case "ground":
                anim.SetBool("Grounded", true);
               
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
                Exitsign.SetActive(true);
                break;
        }
        
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "ground"|| col.gameObject.tag == "movableBox")
            
        anim.SetBool("Grounded", false);
        
    }

    void OnTriggerEnter(Collider col)
    {
     
        switch (col.gameObject.tag)
        {

            case "spike":
              
                source.PlayOneShot(hurt_sound);
                Die(spawn1);
                countdownTimer.timer -= 2f;
    
                break;

            case "spike2": // spawn1 ----> spawn2 tempo ställt till spawn1

                source.PlayOneShot(hurt_sound);
                Die(spawn1); //det är är otroligt lat
                countdownTimer.timer -= 2f;

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
                    
                SceneManager.LoadScene(levelToLoad);
                //countdownTimer.timer = 90f;
                gotKey = false;
                //Application.LoadLevel(SceneManager.);
                
                break;

                    case     "key":
                       Debug.Log("standing on button");
                        col.gameObject.SetActive(false);
                         gotKey= true;
                break;

            case "Level1":
                SceneManager.LoadScene("level1");
                break;

            case "Level2":
                SceneManager.LoadScene("Level2");
                break;
            case "Level3":
                SceneManager.LoadScene("level3(3D)");
                break;

            case "bossLevel":
                SceneManager.LoadScene("BossLevel");
                break;
        }

        
    }

    public void Die(Vector3 spawn)
    {
        Instantiate(blood, transform.position, Quaternion.identity); // spelar upp blood på den "spike" du träffar
        transform.position = spawn;   // spawn
    }

   

    IEnumerator stopBlood()
    {
        yield return new WaitForSeconds(1f);
        blood.GetComponent<ParticleSystem>().enableEmission = false;

    }
   

}