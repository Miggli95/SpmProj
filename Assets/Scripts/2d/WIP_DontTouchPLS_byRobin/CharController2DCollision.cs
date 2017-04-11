using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharController2D {
	public partial class CharController2D : MonoBehaviour {

		[SerializeField] private LayerMask collisionMask;

		private void VerticalCollision(ref Vector3 velocity){
			float directionY = Mathf.Sign (velocity.y);
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;

			for (int i = 0; i < verticalRayCount; i++) {

				Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

				//Debug.DrawRay (raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.green);

				Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.green);


				if (hit) {
					velocity.y = (hit.distance - skinWidth) * directionY;
					rayLength = hit.distance;
				}
			}
		}

		private void HorizontalCollision(ref Vector3 velocity){
			float directionX = Mathf.Sign (velocity.x);
			float rayLength = Mathf.Abs (velocity.x) + skinWidth;

			for (int i = 0; i < horizontalRayCount; i++) {

				Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				//Debug.DrawRay (raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.green);
				Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.green);

				if (hit) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;
				}
			}
		}





		/*
		[SerializeField] private LayerMask collisionMask;


		private void VerticalCollision(ref Vector3 velocity){
			float directionY = Mathf.Sign (velocity.y);
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;

			for (int i = 0; i < verticalRayCount; i++) {
				Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
				if (showRaycast)
					Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.green);
				
				if (hit) {
					velocity.y = (hit.distance - skinWidth) * directionY;
					rayLength = hit.distance;
				}


			}

		}

		private void HorizontalCollision(ref Vector3 velocity){
			float directionX = Mathf.Sign (velocity.x);
			float rayLength = Mathf.Abs (velocity.x) + skinWidth;

			for (int i = 0; i < horizontalRayCount; i++) {
				Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);

				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				if (showRaycast)
					Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.green);

				if (hit) {
					velocity.x = (hit.distance -skinWidth)* directionX;
					rayLength = hit.distance;
				}
			}

		}

*/

	}

}