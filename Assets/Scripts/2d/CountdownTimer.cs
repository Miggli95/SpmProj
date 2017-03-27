using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {
    public string levelToLoad;
    public float timer = 90f;
    private float time;
    public Text timerSecond;
    private Image fillImg;

    // Use this for initialization
     IEnumerator Start() {
        yield return new WaitForSeconds(9f);
        fillImg = this.GetComponent<Image>();
        time = timer;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        fillImg.fillAmount = timer / time;
        timerSecond.text = "Time : "+ timer.ToString("f2");

        if (timer <= 0)
        {
            Application.LoadLevel(levelToLoad);
        }
	}
}
