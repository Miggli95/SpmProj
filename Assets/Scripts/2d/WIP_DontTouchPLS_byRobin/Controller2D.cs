using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterController2D{
	[RequireComponent (typeof (BoxCollider2D))]
	public partial class Controller2D : MonoBehaviour {

		BoxCollider2D playerCollider;

		void Start () {
			playerCollider = GetComponent<BoxCollider2D> ();
			CalculateRaySpacing ();
		}
	
		void Update () {
			
		}


	}

}