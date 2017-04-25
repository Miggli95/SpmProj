using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RayCastController2D
{
	[RequireComponent (typeof(Controller2D))]
	public class Player2D : MonoBehaviour
	{

		[SerializeField] private float movementSpeed = 6;

		[SerializeField]private float jumpHeight = 3.5f;
		[SerializeField]private float timeToJumpApex = 0.3f;
		//time takes to reach the highest point
		float accelerationTimeAirBorne = .2f;
		float accelerationTimeGrounded = .1f;

		float gravity;
		float jumpVelocity;
		Vector3 velocity;
		float velocityXSmoothing;

		Controller2D controller;


		private bool doubleJump;


		void Start ()
		{
			controller = GetComponent<Controller2D> ();

			gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
			jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
			Debug.Log ("<color=green> Player2D Info: </color> Gravity: " + gravity + "; Jump Velocity: " + jumpVelocity);

			doubleJump = false;
		}



		void Update ()
		{

			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}

			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

			if (Input.GetKeyDown (KeyCode.Space)) {
				if (controller.collisions.below) {
					velocity.y = jumpVelocity;
					doubleJump = true;
				} else if (doubleJump) {
					doubleJump = false;
					velocity.y = jumpVelocity;
				}
			}

			//velocity.x = input.x * movementSpeed;
			float targetVelocityX = input.x * movementSpeed;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, 
				(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirBorne);



			velocity.y += gravity * Time.deltaTime;
			controller.Move (velocity * Time.deltaTime);
		}
	}
}