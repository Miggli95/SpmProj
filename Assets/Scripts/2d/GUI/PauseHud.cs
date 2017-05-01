using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHud : MonoBehaviour {
    public GameObject pauseButton,hubButton, pausePanel;
	// Use this for initialization
	void Start () {
        pausePanel.SetActive(false);

    }
	
	// Update is called once per frame
	
    public void OnPause() {
        pausePanel.SetActive(true);
      
        Time.timeScale = 0;

    }
    public void OnUnPause()
    {
        pausePanel.SetActive(false);
        
        Time.timeScale = 1;
    }
    public void LoadHub() {
        Time.timeScale = 1;
        Application.LoadLevel(3);
    }
}
