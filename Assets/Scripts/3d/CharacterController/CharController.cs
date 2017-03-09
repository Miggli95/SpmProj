using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharController : MonoBehaviour
{

    private Vector3 position;
    bool input = false;
    float t;
    CharacterController controller;
    [SerializeField] private float stickToGroundForce;
    [SerializeField] private float gravityMultiplier;
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
    // Use this for initialization
    void Start ()
    {
        position = transform.position;
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        t += Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rotationY = rotationSpeed * Input.GetAxis("Mouse X");
        charinput = new Vector2(horizontal,vertical);
        if (charinput.sqrMagnitude> 1)
        {
            charinput.Normalize();
        }

        transform.Rotate(0, rotationY * rotationSpeed, 0);

        Vector3 destination = Quaternion.Euler(0, rotationY, 0) * ((transform.forward * charinput.y) + (transform.right * charinput.x));
        RaycastHit hit;
      //  Ray ray = new Ray(transform.position, Vector3.down);
        Physics.SphereCast(transform.position,controller.radius,Vector3.down, out hit,
            controller.height/2, Physics.AllLayers,QueryTriggerInteraction.Ignore);

        destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;
        
       moveDir.x = destination.x * speed;
       moveDir.z = destination.z * speed;

        if (!jump)
        {
            jump = Input.GetKeyDown(KeyCode.Space);
        }

        if (!doubleJump && jumping)
        {
            doubleJump = Input.GetKeyDown(KeyCode.Space);
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

        print("Update");
        print("input" + input);
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
}
