using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

enum Abilities
{
    doubleJump = 1,
    slam = 2
}


public class CharController : MonoBehaviour
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
    bool facingForward = true;

    // Use this for initialization
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

    public void Death()
    {
        print("died");

        transform.position = manager.getSpawnPoint();
        transform.localEulerAngles = manager.getRotation();
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

    /*void Update()
{
t += Time.deltaTime;
//slamCollider.SetActive(slam);
float horizontal = Input.GetAxis("Horizontal");
float vertical = Input.GetAxis("Vertical");
charinput = new Vector2(horizontal, vertical);

if (slamTimer > 0.0f)
{
slamTimer -= Time.deltaTime;
if (slamTimer <= 0.0f)
{
aoeSlam.enabled = false;
}
}
if (charinput.sqrMagnitude > 1)
{
charinput.Normalize();
}

if (!lockedRotation)
{
rotationY = rotationSpeed * Input.GetAxis("Mouse X");
transform.Rotate(Vector3.up, rotationY);
}

else
{
transform.rotation = Quaternion.Euler(0,rotationY,0);
}
// transform.Rotate(0, charinput.x * rotationSpeed, 0);

Vector3 destination = transform.forward * charinput.y + transform.right * charinput.x; //Quaternion.Euler(0, transform.rotation.y, 0) * (transform.forward * charinput.y);
RaycastHit hit;
//  Ray ray = new Ray(transform.position, Vector3.down);
Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hit,
controller.height / 2, Physics.AllLayers, QueryTriggerInteraction.Ignore);

destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;

moveDir.x = destination.x * speed;
moveDir.z = destination.z * speed;


if (!jump)
{
if (controller.isGrounded)
{
jump = Input.GetKeyDown(KeyCode.Space);
}
}

if (manager.HaveAbility((int)Abilities.doubleJump))
{
if (!doubleJump && jumping)
{
doubleJump = Input.GetKeyDown(KeyCode.Space);
}
}

if (!previouslyGrounded && controller.isGrounded)
{
jumping = false;
doubleJump = false;
}

previouslyGrounded = controller.isGrounded;
if (controller.isGrounded)
{
moveDir.y -= stickToGroundForce;
//controller.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 1000, transform.position, ForceMode.Impulse);
if (jump)
{
moveDir.y = jumpSpeed;
jump = false;
jumping = true;
}

}
else
{
moveDir += Physics.gravity * gravityMultiplier * Time.deltaTime;
}

if (doubleJump)
{
moveDir.y = doubleJumpSpeed;
jumping = false;
jump = false;
doubleJump = false;
}



colFlags = controller.Move(moveDir * Time.deltaTime);

//print("isSlaming" + slam);
//test code reset progression of gameManager

if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 4)
{

manager.ResetProgression();
}

if (controller.isGrounded)
{
if (slam)
{
Vector3 spawnslam = transform.position;
spawnslam.y -= 1;
aoeSlam.enabled = true;
Instantiate(ShockWave, spawnslam, Quaternion.Euler(90,0,0));
slamTimer = 0.1f;

clip[0].PlayOneShot(SlamSound);
}
moveDir.y = 0;
slam = false;

}

else
{
if (manager.HaveAbility((int)Abilities.slam))
{
if (Input.GetKeyDown(KeyCode.V))
{
moveDir.y = -jumpSpeed;
slam = true;

}
}
}

RaycastHit rayhit;
if (Physics.Raycast(transform.position, Vector3.down, out rayhit))
{

if (rayhit.collider.tag == "enemy" && rayhit.distance < 1.4f)
{
rayhit.collider.GetComponent<EnemyAI3D>().deathAni();
forceJump();
}
if (rayhit.collider.tag == "enemy2" && rayhit.distance < 1.7f)
{
rayhit.collider.GetComponent<Enemy2AI3D>().deathAni();
forceJump();
}
if (rayhit.collider.tag == "enemy3" && rayhit.distance < 1.4f)
{
rayhit.collider.GetComponent<Enemy3AI3D>().deathAni();
forceJump();
}
if (rayhit.collider.tag == "lava" && rayhit.distance < 1.4f)
{
Death();
}
if (rayhit.collider.tag == "spike" && rayhit.distance < 1.5f)
{
Death();
}
}
if (flying)
{
flyingTimer -= Time.deltaTime;
if(flyingTimer <= 0.0f)
{
loadNextBoss();
}
}

}*/


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(vertical));
        charinput = new Vector2(horizontal, vertical);

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

            if (rayhit.collider.tag == "enemy" && rayhit.distance < 0.2f)
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
            rotationY = rotationSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, rotationY);
        }

        /*
        {
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }*/
        // transform.Rotate(0, charinput.x * rotationSpeed, 0);
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
                moveDir.y = jumpSpeed*12;
                bounce = false;
            }
            flyingTimer -= Time.fixedDeltaTime;
            if (flyingTimer <= 0.0f)
            {
                loadNextBoss();
            }
        }
        if (bounce)
        {
            moveDir.y = jumpSpeed;
            bounce = false;
        }

        colFlags = controller.Move(moveDir * Time.fixedDeltaTime);


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
        if(SceneManager.GetActiveScene().name == "BossLevel 3")
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
                SceneManager.LoadScene("BossLevel 2");
                break;
            case "BossLevel 2":
                SceneManager.LoadScene("BossLevel 3");
                break;
            case "BossLevel 3":
                SceneManager.LoadScene("TitleCard");
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
