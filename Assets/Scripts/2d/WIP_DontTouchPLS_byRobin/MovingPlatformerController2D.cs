///* **********************************************
// * 												*
// * 	this is an alt moving platform controller	*
// * 												*
// * 	it has waypoint and raycast system			*
// *	diagonal movement is possible				*
// *  multi(2+) waypoints are possible			*
// * 												*
// * 	ignore collision mask						*
// * 	but setting passenger mask is required		*
// * 												*
// * **********************************************/
//
//
//
//
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//
//namespace RayCastController2D
//{
//	public class MovingPlatformerController2D : Raycast2D
//	{
//
//		struct PassengerMovement
//		{
//			public Transform transform;
//			public Vector3 velocity;
//			public bool standingOnPlatform;
//			public bool moveBeforePlatform;
//
//			public PassengerMovement (Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
//			{
//				transform = _transform;
//				velocity = _velocity;
//				standingOnPlatform = _standingOnPlatform;
//				moveBeforePlatform = _moveBeforePlatform;
//			}
//		}
//
//
//
//		public LayerMask passengerMask;
//
//		[SerializeField] private float platformSpeed;
//		[SerializeField] private float patformWaitTime;
//		[SerializeField] private bool reverseWayPoints;
//
//		private float nextMoveTime;
//		private int fromWayPointIndex;
//		private float percentBetweenWayPoints;
//		//between zero and one
//
//		List<PassengerMovement> passengerMovement;
//		Dictionary<Transform, Controller2D> passengerDictonary = new Dictionary<Transform, Controller2D> ();
//
//		[SerializeField] Vector3[] localWayPoints;
//		private Vector3[] globalWayPoints;
//
//
//
//		public override void Start ()
//		{
//			base.Start ();
//
//			globalWayPoints = new Vector3[localWayPoints.Length];
//			for (int i = 0; i < localWayPoints.Length; i++) {
//				globalWayPoints [i] = localWayPoints [i] + transform.position;
//			}
//
//			if (platformSpeed == 0)
//				Debug.Log ("<color=blue>Moving Platfrom: </color>Warning! Moving platform speed is now equal to zero thus will not move.");
//		}
//
//
//
//		void Update ()
//		{
//			UpdateRayCastOrigins ();
//
//			Vector2 velocity = CalculatePlatformMovement ();
//
//			CalculatePassengerMovement (velocity);
//
//			MovePassengers (true);
//			transform.Translate (velocity);
//			MovePassengers (false);
//		}
//
//
//
//		private void MovePassengers (bool beforeMovePlatform)
//		{
//
//			foreach (PassengerMovement passenger in passengerMovement) {
//
//				if (!passengerDictonary.ContainsKey (passenger.transform)) {
//					passengerDictonary.Add (passenger.transform, passenger.transform.GetComponent<Controller2D> ());
//				}
//				
//				if (passenger.moveBeforePlatform == beforeMovePlatform) {
//					//passenger.transform.GetComponent<Controller2D> ().Move (passenger.velocity, passenger.standingOnPlatform);
//					passengerDictonary [passenger.transform].Move (passenger.velocity, passenger.standingOnPlatform);
//				}
//			}
//
//		}
//
//
//
//		private Vector3 CalculatePlatformMovement ()
//		{
//
//			if (Time.time < nextMoveTime) {
//				return Vector3.zero;
//			}
//
//
//			fromWayPointIndex %= globalWayPoints.Length;
//
//			int toWayPointIndex = (fromWayPointIndex + 1) % globalWayPoints.Length;
//			float distanceBetweenWayPoints = Vector3.Distance (globalWayPoints [fromWayPointIndex], globalWayPoints [toWayPointIndex]);
//
//			percentBetweenWayPoints += Time.deltaTime * platformSpeed / distanceBetweenWayPoints;
//
//			Vector3 newPos = Vector3.Lerp (globalWayPoints [fromWayPointIndex], globalWayPoints [toWayPointIndex], percentBetweenWayPoints);
//
//			if (percentBetweenWayPoints >= 1) {
//				percentBetweenWayPoints = 0;
//				fromWayPointIndex++;
//
//				if (reverseWayPoints) {
//					if (fromWayPointIndex >= globalWayPoints.Length - 1) {
//						fromWayPointIndex = 0;
//						System.Array.Reverse (globalWayPoints);
//					}
//				}
//				nextMoveTime = Time.time + patformWaitTime;
//			}
//
//
//			return newPos - transform.position;
//		}
//
//
//
//		private void CalculatePassengerMovement (Vector3 velocity)
//		{
//			//in case of passengers
//			HashSet<Transform> movedPassengers = new HashSet<Transform> ();
//			passengerMovement = new List<PassengerMovement> ();
//
//
//			float directionX = Mathf.Sign (velocity.x);
//			float directionY = Mathf.Sign (velocity.y);
//
//			#region force push passenger / (mostly) vertical platform
//			if (velocity.y != 0) {
//				float rayLength = Mathf.Abs (velocity.y) + skinWith;
//
//				for (int i = 0; i < verticalRayCount; i++) {
//					Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
//					rayOrigin += Vector2.right * (verticalRaySpacing * i);
//					RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, passengerMask);
//
//					if (hit) {
//						if (!movedPassengers.Contains (hit.transform)) {
//							movedPassengers.Add (hit.transform);
//
//							float pushX = (directionY == 1) ? velocity.x : 0;
//							float pushY = velocity.y - (hit.distance - skinWith) * directionY;
//
//							//hit.transform.Translate (new Vector3 (pushX, pushY));
//							passengerMovement.Add (new PassengerMovement (hit.transform, new Vector3 (pushX, pushY), directionY == 1, true));
//
//						}
//					}
//
//				}
//			}
//			#endregion
//
//			#region force push passenger / (mostly) horizontal platform
//			if (velocity.x != 0) {
//				float rayLength = Mathf.Abs (velocity.x) + skinWith;
//				for (int i = 0; i < horizontalRayCount; i++) {
//					Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
//					rayOrigin += Vector2.up * (horizontalRaySpacing * i);
//					RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, passengerMask);
//
//					if (hit) {
//						if (!movedPassengers.Contains (hit.transform)) {
//							movedPassengers.Add (hit.transform);
//
//							float pushX = velocity.x - (hit.distance - skinWith) * directionX;
//							float pushY = -skinWith;
//
//							//hit.transform.Translate (new Vector3 (pushX, pushY));
//							passengerMovement.Add (new PassengerMovement (hit.transform, new Vector3 (pushX, pushY), false, true));
//						}
//					}
//				}
//			}
//			#endregion
//
//			#region stop passenger jittering / horizontol platform
//			if (directionY == -1 || velocity.y == 0 && velocity.x != 0) {
//				float rayLength = skinWith * 2;
//
//				for (int i = 0; i < verticalRayCount; i++) {
//					Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
//					RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up, rayLength, passengerMask);
//
//					if (hit) {
//						if (!movedPassengers.Contains (hit.transform)) {
//							movedPassengers.Add (hit.transform);
//
//							float pushX = velocity.x;
//							float pushY = velocity.y;
//
//							//hit.transform.Translate (new Vector3 (pushX, pushY));
//							passengerMovement.Add (new PassengerMovement (hit.transform, new Vector3 (pushX, pushY), true, false));
//						}
//					}
//				}
//			}
//			#endregion
//
//
//		}
//
//
//
//		private void OnDrawGizmos ()
//		{
//			if (localWayPoints != null) {
//				Gizmos.color = Color.blue;
//				float size = .3f;
//
//				for (int i = 0; i < localWayPoints.Length; i++) {
//					Vector3 globalWayPointPOS = (Application.isPlaying) ? globalWayPoints [i] : localWayPoints [i] + transform.position;
//					Gizmos.DrawLine (globalWayPointPOS - Vector3.up * size, globalWayPointPOS + Vector3.up * size);
//					Gizmos.DrawLine (globalWayPointPOS - Vector3.left * size, globalWayPointPOS + Vector3.left * size);
//				}
//			}
//		}
//
//	}
//}
