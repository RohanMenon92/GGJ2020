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

    bool joystickPosition, InteractPressed;

    void Start()
    {

    }

    // Control Inputs
    public void ControlButton(string controlName, bool value)
    {
        Debug.Log("Control Button Pressed " + controlName + " Value" + value);
    }

    public void ControlJoystickInput(string controlName, float XValue, float YValue)
    {
        Debug.Log("Control Joystick Input " + controlName + " Value" + XValue + " " + YValue);

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
