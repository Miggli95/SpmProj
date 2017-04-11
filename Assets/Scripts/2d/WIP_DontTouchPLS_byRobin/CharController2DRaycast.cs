using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharController2D {
	public partial class CharController2D : MonoBehaviour {
		
		//[SerializeField] private float skinWidth;
		[SerializeField] private int horizontalRayCount;
		[SerializeField] private int verticalRayCount;


		private BoxCollider2D col;
		private RaycastOrigins raycastOrigins;
		private float horizontalRaySpacing;
		private float verticalRaySpacing;

		private const float skinWidth = 0.1f;


		struct RaycastOrigins {
			public Vector2 topLeft, topRight;
			public Vector2 bottomLeft, bottomRight;
		}

		private void UpdateRaycastOrigins() {
			Bounds bounds = col.bounds;
			bounds.Expand (skinWidth * -2);

			raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
			raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
			raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
			raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
		}

		private void CalculateRayspacing(){
			Bounds bounds = col.bounds;
			bounds.Expand (skinWidth * -2);

			horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
			verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

			horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);

		}

		private void ShowRay(){
			for (int i = 0; i < verticalRayCount; i++) {
				Debug.DrawRay (raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.green);
			}
		}


	}

}