using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharController2D {
	[RequireComponent (typeof (BoxCollider2D))]
	public partial class CharController2D : MonoBehaviour {

		//[SerializeField] private GameManager gameManger;
		[SerializeField] private float gravity = -5;
		[SerializeField] private float movementSpeed = 6;


		#region Action Triggers
		private bool toJump;
		private bool toRun;
		#endregion

		private Vector3 playerVelocity;
		private Vector2 input;

		void Start () {
			toJump = false;
			toRun = false;

			col= GetComponent<BoxCollider2D> ();
			CalculateRayspacing ();
		}


		void Update () {
			input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}

		void FixedUpdate(){
			playerVelocity.y = gravity * Time.deltaTime;
			playerVelocity.x = input.x * movementSpeed * Time.deltaTime;
			Move (playerVelocity);
		}



	}

}