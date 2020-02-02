using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    public float timeLeft = 300.0f;
    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        minutes = (int)Mathf.Floor(timeLeft / 60);
        seconds = (int)timeLeft % 60;
        
        timerText.SetText(string.Format("{0:0}:{1:00}", minutes, seconds));

        if (timeLeft <= 0)
        {
            timerText.SetText("Game Over");
            //trigger game over function
        }
    }
}
