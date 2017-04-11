using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharController2D{
	public partial class CharController2D : MonoBehaviour{
		
		#region Life & Death Situation
		private void Kill(){
			Debug.Log ("Player is dead.");
		}

		private void Respwan(){
			Debug.Log ("Player Respawn.");
		}
			
		#endregion





		private void Reset(){
			Debug.Log ("<color= white> Game Manger:</color> Reset Data.");
			//gameManger.ResetProgression ();
		}
			

	}
}