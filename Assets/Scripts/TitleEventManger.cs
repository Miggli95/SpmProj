/************************************
 * 
 * 	used in Title card
 * 
 * **********************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEventManger : MonoBehaviour {
	
	[SerializeField] private GameObject startMenu;
	[SerializeField] private GameObject exitConfirm;
	[SerializeField] private GameObject loadingScreen;
	[SerializeField] private GameObject hub;



	private GameObject bgImage;
	private List<GameObject> menus = new List<GameObject>();
	private static bool loadHub = false;



	//------------------------------------------
	void Start () {
		if (startMenu == null)
			findObject ("StartMenu", ref startMenu);

		if (exitConfirm == null)
			findObject ("ExitConfirm", ref exitConfirm);

		if (loadingScreen == null)
			findObject ("LoadingScreen", ref loadingScreen);

		if (hub == null)
			findObject ("Hub", ref hub);

		findObject ("bgImage", ref bgImage);

		menus.Add (startMenu);
		menus.Add (exitConfirm);
		menus.Add (loadingScreen);
		menus.Add (hub);

	//	LoadCorrectMenu ("StartMenu");
		if (loadHub)
			LoadCorrectMenu ("Hub");
		else {
			LoadCorrectMenu ("StartMenu");
			loadHub = true;
		}
	}

	void Awake(){
		
	}


	public void findObject(string missingObject, ref GameObject varObject){
		//search in active game objects in scene
		if (GameObject.Find (missingObject)) {
			varObject = GameObject.Find (missingObject);
			Debug.Log ("<color=green>Menu: </color>" + missingObject + " found.");
			return;
		}

		//search in inactive game objects in scene
		foreach(GameObject objectInScene in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]){
			if (objectInScene.name == missingObject) {
				varObject = objectInScene;
				Debug.Log ("<color=green>Menu: </color>" + missingObject + " found as an inactive gameobject.");
				return;
			}
		}

		Debug.Log ("<color=red>Menu Error: </color>Unable to find " + missingObject + ".");

//		//search in prefabs in folder ** aint working, its just here forfuture...
//		try {
//			varObject = Resources.Load (missingObject) as GameObject;
//		} catch (UnityException e) {
//			Debug.Log ("<color=red>Menu Error: </color>Unable to find " + missingObject + ".");
//		}
//		varObject = Instantiate (Resources.Load (missingObject, typeof(GameObject))) as GameObject;
//		Debug.Log ("<color=green>Menu: </color>" + missingObject + " found as a prefab.");
	}



	//--------------------------------------------

	public void LoadCorrectMenu(string menuName){
		if (menuName.Equals ("hub", StringComparison.InvariantCultureIgnoreCase)) {
			bgImage.SetActive (false);
		} else {
			bgImage.SetActive (true);
		}

		foreach (GameObject obj in menus) {
			if (obj.name.Equals (menuName, StringComparison.InvariantCultureIgnoreCase)) {
				obj.SetActive (true);
			} else
				obj.SetActive (false);
		}
	}
		

	public void ExitGame(){
		if (UnityEditor.EditorApplication.isPlaying)
			UnityEditor.EditorApplication.isPlaying = false;
		else
			Application.Quit ();
	}



	//-------------------------------------------



}
