using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour {

    public int levelNumber;
    public float bestScore;        // The player's score.
    public float yourScore;
    public GameManager manager;
    public Text yourScoreText;                      // Reference to the Text component.
    public Text bestScoreText;
    void Start()
    {
        bestScore = manager.getBestScore(levelNumber);
        yourScore = manager.getYourScore(levelNumber);
        
    }

    void Update()
    {
        // Set the displayed text 

        yourScoreText.text =  "" + yourScore;
        bestScoreText.text = "" + bestScore; 
    }
    
}

   

