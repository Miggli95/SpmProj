using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterController2D{
	public partial class Controller2D : MonoBehaviour {

		struct RaycastOrigins{
			public Vector2 topLeft;
			public Vector2 topRight;
			public Vector2 bottomLeft;
			public Vector2 bottomRight;
		}



		RaycastOrigins raycastOrigins;
		const float skinWith = 0.02f;

		[SerializeField] private int horizontalRayCount = 4;
		[SerializeField] private int verticalRayCount = 4;

		private float horizontalRaySpacing;
		private float verticalRaySpacing;


		private void UpdateRayCastOrigins(){
			Bounds bounds = playerCollider.bounds;
			bounds.Expand (skinWith * (-2));

			raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
			raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
			raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
			raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		}


		private void CalculateRaySpacing(){
			Bounds bounds = playerCollider.bounds;
			bounds.Expand (skinWith * (-2));

			horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, 20);		//change 20 to int.MaxValue to remove limit
			verticalRayCount = Mathf.Clamp (verticalRayCount, 2, 20);


			horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
		}




	}
}