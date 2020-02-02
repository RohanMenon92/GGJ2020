using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    private int minutes;
    private int seconds;
    private GameManager gManager;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        gManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = gManager.levelTime;
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
