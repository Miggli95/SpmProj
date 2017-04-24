using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RayCastController2D
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class Raycast2D : MonoBehaviour
	{

		[HideInInspector] BoxCollider2D playerCollider;
		public LayerMask collisionMask;

		public int horizontalRayCount = 4;
		public int verticalRayCount = 4;

		[HideInInspector] public float horizontalRaySpacing;
		[HideInInspector] public float verticalRaySpacing;

		[HideInInspector] public RaycastOrigins raycastOrigins;
		[HideInInspector] public const float skinWith = 0.02f;



		public struct RaycastOrigins
		{
			public Vector2 topLeft;
			public Vector2 topRight;
			public Vector2 bottomLeft;
			public Vector2 bottomRight;
		}




		public virtual void Start ()
		{
			playerCollider = GetComponent<BoxCollider2D> ();
			CalculateRaySpacing ();
		}




		public void UpdateRayCastOrigins ()
		{
			Bounds bounds = playerCollider.bounds;
			bounds.Expand (skinWith * (-2));

			raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
			raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
			raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
			raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		}


		public void CalculateRaySpacing ()
		{
			Bounds bounds = playerCollider.bounds;
			bounds.Expand (skinWith * (-2));

			horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, 20);		//change 20 to int.MaxValue to remove limit
			verticalRayCount = Mathf.Clamp (verticalRayCount, 2, 20);


			horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
		}


	}
}
