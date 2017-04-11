using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharController2D{
	public partial class CharController2D : MonoBehaviour{
		/*
		[SerializeField] private float runSpeed;

		private float movementDirection;


		private void Run(Vector3 velocity){
			UpdateRaycastOrigins ();
			VerticalCollision (ref velocity);
			transform.Translate (velocity);
		}
			/*
			movementDirection = Input.GetAxis ("Horizontal");

			if (movementDirection > 0)
				//facing right
				transform.eulerAngles = new Vector2 (0, 0);
			else if (movementDirection < 0)
				//facing left
				transform.eulerAngles = new Vector2 (0, 180);

			transform.Translate (Vector2.right * runSpeed * Time.deltaTime);*/

		

		private void Move (Vector3 velocity){
			UpdateRaycastOrigins ();
			if (velocity.x != 0)
				HorizontalCollision (ref velocity);
			if (velocity.y != 0)
				VerticalCollision (ref velocity);

			transform.Translate (velocity);
		}



	}
}