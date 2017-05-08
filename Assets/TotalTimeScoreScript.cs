using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalTimeScoreScript : MonoBehaviour
{ 


    public int levelNumber;
    public float bestTotalTime;        // The player's score.
    public float totalTime;
    public float currentTime;
    public GameManager manager;
    public Text totalTimeText;                      // Reference to the Text component.
    public Text bestTotalTimeText;
    public Text currentTimeText;
    void Start()
    {
        bestTotalTime = manager.getBestTotalTime();
        totalTime = manager.getTotalTime();
        currentTime = manager.getCurrentTime();

    }

    void Update()
    {
        // Set the displayed text 
        bestTotalTimeText.text = "" + bestTotalTime;
        totalTimeText.text = "" + totalTime;
        currentTimeText.text = "" + currentTime;
    }
}



