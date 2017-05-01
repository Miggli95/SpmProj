using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

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
    }
}
