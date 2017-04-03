using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScriptSlamScene : MonoBehaviour {


    public float timerTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timerTime += Time.deltaTime;

        if(timerTime>8)
        {
            SceneManager.LoadScene(7);
        }
		
	}
}
