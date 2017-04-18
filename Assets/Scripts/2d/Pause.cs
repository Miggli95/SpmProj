using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    private bool paused = false;
    

    void Update()
    {
        PauseHud ph = GetComponent<PauseHud>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
           ph.OnPause();
            
        }
        else
        {
            ph.OnUnPause();
           
        }
    }
}
