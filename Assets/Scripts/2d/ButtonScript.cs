using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
        player2d_controller player2 = player.GetComponent<player2d_controller>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clicked()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Return button")
        {
            SceneManager.LoadScene(3);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Repeat button")
        {
            if (player.GetComponent<player2d_controller>().retry1 == true)
            {
                SceneManager.LoadScene(0);
                player.GetComponent<player2d_controller>().retry1 = false;
            }
        }
    }
}
