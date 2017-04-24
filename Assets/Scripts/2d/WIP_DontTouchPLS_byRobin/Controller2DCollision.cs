using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterController2D{
	
	public partial class Controller2D : MonoBehaviour {

		public struct CollisionInfo	{
			public bool above;
			public bool below;
			public bool left;
			public bool right;

			public void Reset(){
				above = false; 
				below = false;
				left = false;
				right = false;
			}
		}




		[SerializeField] private LayerMask collisionMask;
		public CollisionInfo collisions;


		public void Move (Vector3 velocity){
			UpdateRayCastOrigins ();
			collisions.Reset ();

			if (velocity.x != 0)
			HorizontalCollisions (ref velocity);
			if (velocity.y != 0)
			VerticalCollisions (ref velocity);

			transform.Translate (velocity);
		}


		private void VerticalCollisions(ref Vector3 velocity){
			float directionY = Mathf.Sign (velocity.y);
			float rayLength = Mathf.Abs (velocity.y) + skinWith;
			for (int i = 0; i<verticalRayCount; i++){
				Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

				//Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right*verticalRaySpacing*i, Vector2.up*(-2), Color.green);
				Debug.DrawRay(rayOrigin, Vector2.up*directionY, Color.green);

				if (hit) {
					velocity.y = (hit.distance - skinWith) * directionY;
					rayLength = hit.distance;

					collisions.below = directionY == -1;
					collisions.above = directionY == 1;
				}
			}

		}

		private void HorizontalCollisions(ref Vector3 velocity){
			float directionX = Mathf.Sign (velocity.x);
			float rayLength = Mathf.Abs (velocity.x) + skinWith;
			for (int i = 0; i<horizontalRayCount; i++){
				Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);

				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				//Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right*verticalRaySpacing*i, Vector2.up*(-2), Color.green);
				Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.green);

				if (hit) {
					velocity.x = (hit.distance - skinWith) * directionX;
					rayLength = hit.distance;

					collisions.left = directionX == -1;
					collisions.right = directionX == 1;

				}
			}

		}




	}

}