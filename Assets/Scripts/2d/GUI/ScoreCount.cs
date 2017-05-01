using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

    public int score = 0 ;        // The player's score.


    Text text;                      // Reference to the Text component.


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
        text.text = " x " + score;
    }
    public void AddScore() {
        Debug.Log("Dead");
        score += score +1;

    }
}
