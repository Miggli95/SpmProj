using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramrateCAP : MonoBehaviour {
    public int vSync;
    public int frameRate;

    Resolution res;
    // Use this for initialization
    void Awake () {
        res = Screen.currentResolution;
        if (res.refreshRate == 60) {
            QualitySettings.vSyncCount = 1;
           
        }
        if (res.refreshRate == 120)
        {
            QualitySettings.vSyncCount = 2;
        }

        Debug.Log(QualitySettings.vSyncCount);
        //QualitySettings.vSyncCount = vSync;
        //Application.targetFrameRate = frameRate;
        }
	
	
}
