using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ProgressBarMode
{
    HIDDEN,
    NOBG,
    WITHBG
}

public class StationProgressBar : MonoBehaviour
{
    public ProgressBarMode mode = ProgressBarMode.HIDDEN;

    public Image healthBar;
    public Image background;
    public void updateBar(float value, float maxValue)
    {
        healthBar.fillAmount = (float)value / (float)maxValue;
    }

    public void setStatusBarMode(ProgressBarMode newMode)
    {
        mode = newMode;
        if (mode == ProgressBarMode.HIDDEN)
        {
            var tempColor = healthBar.color;
            tempColor.a = 0f;
            healthBar.color = tempColor;

            tempColor = background.color;
            tempColor.a = 0f;
            background.color = tempColor;
        }
        else if (mode == ProgressBarMode.NOBG)
        {
            var tempColor = healthBar.color;
            tempColor.a = 0.65f;
            healthBar.color = tempColor;

            tempColor = background.color;
            tempColor.a = 0f;
            background.color = tempColor;
        }
        else if (mode == ProgressBarMode.WITHBG)
        {
            var tempColor = healthBar.color;
            tempColor.a = 0.8f;
            healthBar.color = tempColor;

            tempColor = background.color;
            tempColor.a = 0.8f;
            background.color = tempColor;
        }
    }
}
