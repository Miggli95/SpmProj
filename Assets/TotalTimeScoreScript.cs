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
		if(bestTotalTime == 0)
			bestTotalTimeText.text = "N/A";
		else
        	bestTotalTimeText.text = "" + bestTotalTime;
        
		if(totalTime == 0)
			totalTimeText.text = "N/A";
		else
			totalTimeText.text = "" + totalTime;
        
		if(currentTime == 0)
			currentTimeText.text = "N/A";
		else
			currentTimeText.text = "" + currentTime;
    }
}



