using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramrateCAP : MonoBehaviour {
    public int vSync;
    public int frameRate;
    // Use this for initialization
    void Awake () {
        QualitySettings.vSyncCount = vSync;
        Application.targetFrameRate = frameRate;
        }
	
	
}
