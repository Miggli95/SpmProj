using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour {

    public GameObject player;
    public GameObject retryMenu;
	// Use this for initialization
	void Start () {
        CharController2D player2 = player.GetComponent<CharController2D>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void clicked()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Return button")
        {
            Time.timeScale = 1;
            retryMenu.SetActive(false);
           
            player.GetComponent<CharController2D>().retry1 = false;
            player.GetComponent<CharController2D>().retry2 = false;
            player.GetComponent<CharController2D>().retry3 = false;
            player.GetComponent<CharController2D>().retry4 = false;
            retryMenu.SetActive(false);


        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Repeat button")
        {
            if (player.GetComponent<CharController2D>().retry1 == true)
            {
                SceneManager.LoadScene(0);
                player.GetComponent<CharController2D>().retry1 = false;
            }
            else if(player.GetComponent<CharController2D>().retry2 == true)
            {
                SceneManager.LoadScene(1);
                player.GetComponent<CharController2D>().retry2 = false;
            }
            else if (player.GetComponent<CharController2D>().retry3 == true)
            {
                SceneManager.LoadScene(2);
                player.GetComponent<CharController2D>().retry3 = false;
            }
            else if (player.GetComponent<CharController2D>().retry4 == true)
            {
                SceneManager.LoadScene(4);
                player.GetComponent<CharController2D>().retry4 = false;
            }
        }
    }
}
