using DG.Tweening;
using System;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public int controllerId;
    public ProductBox carryingBox = null;
    public Transform carryBoxPos;
    public float movingSpeed = 0.2f;
    public float rotationSpeed = 5f;
    public GameConstants.PlayerBotType botType;
    public GameObject modelReference;
    // Start is called before the first frame update

    bool isWorking;
    StationTop workStation;

    bool isSprinting, isInteracting1, isInteracting2;
    bool special1Pressed, special2Pressed, sprintPressed;
    bool joystickMoving;

    StationTop canInteractStation;

    Vector2 joystickPosition;

    void Start()
    {
        
    }

    public void GetBox(ProductBox getBox)
    {
        carryingBox = getBox;
        getBox.MoveBoxTo(carryBoxPos);
    }

    public void BoxGiven()
    {
        carryingBox = null;
    }

    public void StartWork(StationTop moveTo)
    {
        isWorking = true;
        workStation = moveTo;
        this.transform.DOMove(moveTo.workTransform.position, 0.5f).SetEase(Ease.InOutSine);
        this.transform.DORotate(moveTo.workTransform.rotation.eulerAngles, 0.5f).SetEase(Ease.InOutSine);
        // Switch to working animation
    }

    // Control Inputs
    public void ControlButton(GameConstants.ButtonMessage buttonControl, bool value)
    {
        Debug.Log("Control Button Pressed " + buttonControl + " Value" + value);

        switch (buttonControl)
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

    public void ControlJoystickInput(GameConstants.JoystickControlMessage joystickControl, float XValue, float YValue)
    {
        switch(joystickControl)
        {
            case GameConstants.JoystickControlMessage.MoveJoystick:
                MoveJoystick(XValue, YValue);
                break;
            default:
                break;
        }
    }

    public void ControlJoystickToggle(GameConstants.JoystickControlMessage joystickControl, bool value)
    {
        Debug.Log("Control Joystick Toggle " + joystickControl.ToString() + " Value" + value);
               
        switch (joystickControl)
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
            joystickPosition = new Vector2(xVal, -yVal);
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

        if (collStationTop != null)
        {
            // Is the correct Job
            canInteractStation = collStationTop;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Set the station that can be interacted with and the box that can be interacted with

        StationTop collStationTop = collision.gameObject.GetComponent<StationTop>();

        if(collStationTop != null && canInteractStation != null)
        {
            if (collStationTop == canInteractStation)
            {
                // No need to check if station is the same
                return;
            }

            float collStationDistance = Vector3.Distance(collStationTop.transform.position, transform.position);
            float currStationDistance = Vector3.Distance(canInteractStation.transform.position, transform.position);
            if (collStationDistance < currStationDistance)
            {
                canInteractStation = collStationTop;
            }
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
    }

    //Game Code
    // Update is called once per frame
    void Update()
    {

        // TODO: Sendsignal over to Station Tops for interaction

        // HAVE MINIGAME CONTROLS HERE
        // If Carrying a box
        if (isInteracting1 || isInteracting2)
        {
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
            }
            else
            {
                // If not carrying a box
                if (canInteractStation != null && canInteractStation.currentBox != null)
                {
                    if(canInteractStation.progress >= 1f)
                    {
                        canInteractStation.Interact(this, null);
                    } else if (canInteractStation.isWorking == false)
                    {
                        canInteractStation.Interact(this, null);
                    }
                }
            }
        }
        // Check Joystick Control
        if(isWorking)
        {
            if(isSprinting)
            {
                // Cancel Work, change anim
                isWorking = false;
                workStation.Pause();
                workStation = null;
            }
        }

        if (joystickMoving && !isWorking)
        {
            // handle movement
            Vector2 directionToMove = joystickPosition * (movingSpeed * (isSprinting ? 1.5f : 1f));

            // Set movement limits
            if((transform.position.z < -3.5f && directionToMove.y < 0) || (transform.position.z > 50f && directionToMove.y > 0))
            {
                directionToMove.y = 0f;
            }
            if ((transform.position.x < -29f && directionToMove.x < 0) || (transform.position.x > 29f && directionToMove.x > 0)) {
                directionToMove.x = 0f;
            }

            // TODO:: Should be rigidbody physics?
            Debug.Log("TRANSLATING");
            gameObject.transform.Translate(new Vector3(directionToMove.x, 0, directionToMove.y));

            Vector3 dir = new Vector3(directionToMove.x, transform.position.y, directionToMove.y);
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            Debug.Log("ROTATING");
            modelReference.transform.localRotation = Quaternion.Slerp(modelReference.transform.localRotation, rot, rotationSpeed * Time.deltaTime);
        }
    }
}
