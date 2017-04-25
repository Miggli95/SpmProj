/**************************************************
 * 
 * 	Require BoxCollider2D and only BoxCollider2D
 * 
 * 	- Single/Double jump with variable height 
 * 
 * ************************************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RayCastController2D
{
	[RequireComponent (typeof(CollisionController2D))]
	public class Player2D : MonoBehaviour
	{

		[SerializeField] private float movementSpeed = 6;

		[SerializeField] private float maxJumpHeight = 3.5f;
		[SerializeField] private float minJumpHeight = 0.3f;

		[SerializeField]private float timeToJumpApex = 0.3f;
		//time takes to reach the highest point
		float accelerationTimeAirBorne = .2f;
		float accelerationTimeGrounded = .1f;

		private float gravity;
		private float maxJumpVelocity;
		private float minJumpVelocity;

		Vector3 velocity;
		float velocityXSmoothing;

		CollisionController2D collisionController;


		private bool doubleJump;


		void Start ()
		{
			collisionController = GetComponent<CollisionController2D> ();

			gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
			maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
			minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

			Debug.Log ("<color=green> Player2D Info: </color> Gravity: " + gravity + "; Max Jump Velocity: " + maxJumpVelocity);

			doubleJump = false;
		}



		void Update ()
		{

			if (collisionController.collisions.above || collisionController.collisions.below) {
				velocity.y = 0;
			}

			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));


			if (Input.GetKeyDown (KeyCode.Space)) {
				if (collisionController.collisions.below) {
					velocity.y = maxJumpVelocity;
					doubleJump = true;
				} else if (doubleJump) {
					doubleJump = false;
					velocity.y = maxJumpVelocity;
				}
			}


			if (Input.GetKeyUp (KeyCode.Space)) {
				if (velocity.y > minJumpVelocity) {
					velocity.y = minJumpVelocity;
				}
			}


			//velocity.x = input.x * movementSpeed;
			float targetVelocityX = input.x * movementSpeed;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, 
				(collisionController.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirBorne);

			velocity.y += gravity * Time.deltaTime;
			collisionController.Move (velocity * Time.deltaTime);
		}
	}
}