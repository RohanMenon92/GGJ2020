using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxPanel : MonoBehaviour
{
    public Image boxIcon;
    public Transform productionIconsTransform;
    public List<Image> productionIcons;

    public Sprite[] uninspectedAssets;
    public Sprite[] inspectedAssets;

    private Image backgroundImage;
    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        //showAllProductionIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showAllProductionIcons()
    {
        Animator[] productionIconsAnimators = GetComponentsInChildren<Animator>();
        foreach(Animator animator in productionIconsAnimators)
        {
            animator.SetTrigger("FadeIn");
        }
    }

    public void updateUI(List<GameConstants.StationType> processes, int currentWork, bool inspected)
    {
        if(inspected)
        {
            backgroundImage.sprite = inspectedAssets[0];
            boxIcon.sprite = inspectedAssets[1];
        }
        else
        {
            backgroundImage.sprite = uninspectedAssets[0];
            boxIcon.sprite = uninspectedAssets[1];
        }
        for(int i = 1; i < processes.Count; i++)
        {
            GameConstants.StationType type = processes[i];
            Image imageObject = productionIcons[i];
            if (i < currentWork)
            {
                // check icon
            }
            else
            {
                // work icon
                switch (type)
                {
                    case GameConstants.StationType.LElectric:
                        imageObject.color = new Color(0.5f, 0f, 0f);
                        Debug.Log("Electric Task");
                        break;
                    case GameConstants.StationType.LProgramming:
                        imageObject.color = new Color(0.5f, 0.5f, 0f);
                        Debug.Log("Programming Task");
                        break;
                    case GameConstants.StationType.HWelding:
                        imageObject.color = new Color(0.5f, 0.5f, 0.5f);
                        Debug.Log("Welding Task");
                        break;
                    case GameConstants.StationType.HWiring:
                        imageObject.color = new Color(0f, 0.5f, 0.5f);
                        Debug.Log("Wiring Task");
                        break;
                    case GameConstants.StationType.CPolishing:
                        imageObject.color = new Color(0f, 0f, 0.5f);
                        Debug.Log("Polishing Task");
                        break;
                }
            }
        }
    }
}
