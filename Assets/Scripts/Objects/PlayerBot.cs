using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public int controllerId;
    public ProductBox carryingBox = null;
    public int movingSpeed = 10;
    public GameConstants.PlayerBotType botType;
    // Start is called before the first frame update

    bool isSprinting, isInteracting1, isInteracting2;
    bool special1Pressed, special2Pressed, sprintPressed;
    bool joystickMoving;

    ProductBox canInteractBox;
    StationTop canInteractStation;

    GameObject interactCollider;

    Vector2 joystickPosition;

    
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
        GameConstants.JoystickControlMessage result = (GameConstants.JoystickControlMessage
            )Enum.Parse(typeof(GameConstants.ButtonMessage), controlName, true);

        switch(result)
        {
            case GameConstants.JoystickControlMessage.MoveJoystick:
                MoveJoystick(XValue, YValue);
                break;
            default:
                break;
        }
    }

    public void ControlJoystickToggle(string controlName, bool value)
    {
        Debug.Log("Control Joystick Toggle " + controlName + " Value" + value);

        GameConstants.JoystickControlMessage result = (GameConstants.JoystickControlMessage
           )Enum.Parse(typeof(GameConstants.ButtonMessage), controlName, true);

        switch (result)
        {
            case GameConstants.JoystickControlMessage.MoveJoystick:
                ToggleJoystick(value);
                break;
            default:
                break;
        }

    }

    public void MoveJoystick(float xVal, float yVal)
    {
        // Only Move Joystick if joystick toggle is recieved
        if(joystickMoving)
        {
            joystickPosition = new Vector2(xVal, yVal);
        }
    }

    public void ToggleJoystick(bool isTouch)
    {
        // Toggle Joystick to affect position
        joystickMoving = isTouch;
        if(!isTouch)
        {
            joystickPosition = new Vector2(0, 0);
        }
    }

    bool isCarryingBox()
    {
        return carryingBox != null;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Set the station that can be interacted with and the box that can be interacted with

        StationTop collStationTop = collision.gameObject.GetComponent<StationTop>();

        if (isCarryingBox() && collStationTop != null)
        {
            // Is the correct Job
            if (carryingBox.GetCurrentJob() == collStationTop.stationType)
            {
                canInteractStation = collStationTop;
            }
        }

        ProductBox collBox = collision.gameObject.GetComponent<ProductBox>();

        if (!isCarryingBox() && collBox != null)
        {
            canInteractBox = collBox;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Remove station being interacted with and box being interacted with
        StationTop collStation = collision.gameObject.GetComponent<StationTop>();

        if (collStation != null && collStation == canInteractStation)
        {
            canInteractStation = null;
        }

        ProductBox collBox = collision.gameObject.GetComponent<ProductBox>();

        if (collBox != null && canInteractBox == collBox)
        {
            canInteractBox = null;
        }
    }

    //Game Code
    // Update is called once per frame
    void Update()
    {

        // TODO: Sendsignal over to Station Tops for interaction

        if (carryingBox)
        {
            if (canInteractStation != null && isInteracting1)
            {
                carryingBox.isCarried = false;
                carryingBox.isPlaced = true;
                canInteractStation.Interact(this, carryingBox);
            }

            if (canInteractStation != null && isInteracting2)
            {
                carryingBox.isCarried = false;
                carryingBox.isPlaced = true;
                canInteractStation.Interact(this, carryingBox);
            }
        } else
        {
            if(canInteractBox && isInteracting1)
            {
                canInteractBox.isCarried = true;
                canInteractBox.isPlaced = false;
            }
        }

        // Check Joystick Control
        if(joystickMoving)
        {
            Vector2 directionToMove = joystickPosition * movingSpeed;

            // TODO:: Should be rigidbody physics?
            gameObject.transform.Translate(directionToMove);
        }
    }
}
