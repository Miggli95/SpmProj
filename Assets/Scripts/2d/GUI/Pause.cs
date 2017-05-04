using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
 /*
	private bool paused = false;
    GameObject canvas;

    private void Start()
    {
        canvas = GameObject.FindObjectOfType<PauseHud>().gameObject;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoPause();
        }


    }


    void Flip()
    {
        paused = !paused;
    }

    void GoPause()
    {
        PauseHud ph = canvas.GetComponent<PauseHud>();

        if (paused)
        {
            Flip();
            ph.OnPause();
        }
        else
        {
            Flip();
            ph.OnUnPause();
        }
    }*/

	[SerializeField] private GameObject pauseMenu;
	private bool paused;

	void Start(){
		if (pauseMenu == null) {
			findPauseMenu (ref pauseMenu);
		}

		paused = false;
		pauseMenu.SetActive(false);
	}

	void Awake(){
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
			if (paused)
				ShowPauseMenu ();
			else
				HidePauseMenu ();
		}
	}



	public void ShowPauseMenu(){
		paused = true;
		Time.timeScale = 0;
		pauseMenu.SetActive (true);
		Debug.Log ("<color=green>Pause Menu: </color>Paused.");
	}

	public void HidePauseMenu(){
		paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
		Debug.Log ("<color=green>Pause Menu: </color>Unpaused.");
	}

	public void LoadHub(){
		paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
		Application.LoadLevel(0);
	}




	private void findPauseMenu(ref GameObject varMenu){
		if (GameObject.Find ("PausePanel")) {
			varMenu = GameObject.Find ("PausePanel");
			Debug.Log ("<color=green>Menu: </color>Pause menu found.");
			return;
		}

		//search in inactive game objects in scene
		foreach(GameObject objectInScene in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]){
			if (objectInScene.name == "PausePanel") {
				varMenu = objectInScene;
				Debug.Log ("<color=green>Menu: </color>Pause menu found as an inactive gameobject.");
				return;
			}
		}

		throw new ArgumentNullException ("<color=red>Pause Menu Error: </color>No pause menu found.");
	}




}
