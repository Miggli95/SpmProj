using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharController2D{
	public partial class CharController2D : MonoBehaviour{

		private void Jump(){
			if (toJump == true) {
				Debug.Log ("Jump");
				toJump = false;
			}
		}
			



	}
}