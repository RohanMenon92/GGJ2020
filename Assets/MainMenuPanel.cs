using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;

public class MainMenuPanel : MonoBehaviour
{
    public Animator animator;
    public bool creditShowing;
    public bool fading;
    public GameManager gManager;
    public TMP_Text firstGuideText;
    public Image firstGuideIcon;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (AirConsole.instance.IsAirConsoleUnityPluginReady() && AirConsole.instance.GetControllerDeviceIds().Count >= 2)
        {
            firstGuideText.text = "press            to begin";
            firstGuideIcon.gameObject.SetActive(true);
            
        }
        else
        {
            firstGuideText.text = "more player needed.";
            firstGuideIcon.gameObject.SetActive(false);
        }
    }

    public void toggleCredit()
    {
        if (!fading)
        {
            if (creditShowing)
            {
                animator.SetTrigger("HideCredit");
                creditShowing = false;
            }
            else
            {
                animator.SetTrigger("ShowCredit");
                creditShowing = true;
            }
        }
    }

    public void fadeToGame()
    {
        if (!creditShowing && AirConsole.instance.GetControllerDeviceIds().Count >= 2)
        {
            fading = true;
            animator.SetTrigger("FadeToGame");
        }
    }

    public void fadeFinished()
    {
        gameObject.SetActive(false);
        gManager.mainMenuShowing = false;
        gManager.startGame();
    }
}
