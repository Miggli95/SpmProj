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
    private float flyingTimer = 4.0f;
    private bool flying = false;
    public GameObject slamCollider;

    public GameObject SlamEffect;




    public Vector3 spawn1, spawn2;





    // Use this for initialization
    void Start()
    {

        //Death();
        position = transform.position;
        controller = GetComponent<CharacterController>();
        rotationY = transform.rotation.y;
        aoeSlam = GetComponent<SphereCollider>();


        // Death();
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
    void Update()
    {
        t += Time.deltaTime;
        //slamCollider.SetActive(slam);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rotationY = rotationSpeed * Input.GetAxis("Mouse X");
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

        transform.Rotate(Vector3.up, rotationY);

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
            moveDir += Physics.gravity * gravityMultiplier * Time.fixedDeltaTime;
        }

        if (doubleJump)
        {
            moveDir.y = doubleJumpSpeed;
            jumping = false;
            jump = false;
            doubleJump = false;
        }



        colFlags = controller.Move(moveDir * Time.fixedDeltaTime);

        //print("isSlaming" + slam);
        //test code reset progression of gameManager
        if (Input.GetKeyDown(KeyCode.R))
        {

            manager.ResetProgression();
        }

        if (controller.isGrounded)
        {
            if (slam)
            {
                aoeSlam.enabled = true;
                slamTimer = 0.1f;
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
                    Instantiate(SlamEffect, transform.position, Quaternion.identity);
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
            if (rayhit.collider.tag == "enemy2" && rayhit.distance < 1.5f)
            {
                rayhit.collider.GetComponent<Enemy2AI3D>().deathAni();
                forceJump();
            }
            if (rayhit.collider.tag == "enemy3" && rayhit.distance < 1.4f)
            {
                rayhit.collider.GetComponent<Enemy3AI3D>().deathAni();
                forceJump();
            }
            if (rayhit.collider.tag == "lava" && rayhit.distance < 1.1f)
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
        moveDir.y = jumpSpeed;
    }
    public void doSuperJump()
    {
        flying = true;
        moveDir.y = jumpSpeed*12;

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


}
