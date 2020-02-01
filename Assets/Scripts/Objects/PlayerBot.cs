using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public int controllerId;
    public ProductBox carryingBox = null;
    public int movingSpeed;
    public GameConstants.PlayerBotType botType;
    // Start is called before the first frame update

    bool isSprinting;
    bool isInteracting1;
    bool isInteracting2;

    Vector2 joystickPosition;

    bool special1Pressed, special2Pressed, sprintPressed;

    void Start()
    {

    }

    // Control Inputs
    public void ControlButton(string controlName, bool value)
    {
        Debug.Log("Control Button Pressed " + controlName + " Value" + value);

        // Using this to break up 
        GameConstants.ButtonMessage result = (GameConstants.ButtonMessage)Enum.Parse(typeof(GameConstants.ButtonMessage), controlName, true);

        Debug.Log("Valid Action Pressed " + result);

        switch (result)
        {
            case GameConstants.ButtonMessage.Special1Pressed:
                Special1Pressed(value);
                break;
            case GameConstants.ButtonMessage.Special2Pressed:
                Special2Pressed(value);
                break;
            case GameConstants.ButtonMessage.SprintPressed:
                SprintPressed(value);
                break;
        }
        
    }

    private void SprintPressed(bool isPressed)
    {
        if(!isSprinting && isPressed)
        {
            isSprinting = true;
        } else if(isSprinting && !isPressed)
        {
            isSprinting = false;
        }
    }

    private void Special2Pressed(bool isPressed)
    {
        if (!isInteracting2 && isPressed)
        {
            isInteracting2 = true;
        }
        else if (isInteracting2 && !isPressed)
        {
            isInteracting2 = false;
        }
    }

    private void Special1Pressed(bool isPressed)
    {
        if (!isInteracting1 && isPressed)
        {
            isInteracting1 = true;
        }
        else if (isInteracting1 && !isPressed)
        {
            isInteracting1 = false;
        }
    }

    public void ControlJoystickInput(string controlName, float XValue, float YValue)
    {
        Debug.Log("Control Joystick Input " + controlName + " Value" + XValue + " " + YValue);
        // Switch for joystick
    }

    public void ControlJoystickToggle(string controlName, bool value)
    {

        Debug.Log("Control Joystick Toggle " + controlName + " Value" + value);
    }


    //Game Code
    // Update is called once per frame
    void Update()
    {
        if (carryingBox)
        {
            // the box should be in front of player while carrying
            //carryingBox.transform.position = ;
        }
    }
}
