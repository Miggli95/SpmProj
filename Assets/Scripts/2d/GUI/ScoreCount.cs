using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

    private int score = 0 ;        // The player's score.

    public GameManager manager;
    Text text;                      // Reference to the Text component.

    void Start()
    {
        score = manager.getDeathCount();
    }
    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
       // score = 0;
    }


    void Update()
    {
        // Set the displayed text 
        if (score == 0)
        {
            score = manager.getDeathCount();
        }
        text.text = " x " + score;
    }
    public void AddScore() {
        Debug.Log("Dead");
        score ++;
        manager.SetDeathCount(score);

    }
}
