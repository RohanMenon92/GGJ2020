using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class UserControl : MonoBehaviour
{
    public PlayerBot smithBot;
    public PlayerBot sparkyBot;

    public Image smithRequired;
    public Image sparkyRequired;

    // Private
    GameManager gameManager;


    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += AddNewPlayer;
        AirConsole.instance.onMessage += InputPressed;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    void OnReady(string code)
    {
        Debug.Log("On ready called with code " + code);
    }

    public void AddNewPlayer(int deviceID)
    {
        /*
         Debug.Log("New Player Added " + deviceID);

        if (devicesConnected.ContainsKey(deviceID))
        {
            return;
        }

        PlayerBot newPlayerBot = gameManager.OnPlayerAdded();

        devicesConnected.Add(deviceID, newPlayerBot);
        */

        if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0)
        {
            if (AirConsole.instance.GetControllerDeviceIds().Count >= 2)
            {
                smithBot = gameManager.GetSmithBot();
                sparkyBot = gameManager.GetSparkyBot();
                StartGame();
            }
            else
            {
                //uiText.text = "NEED MORE PLAYERS";
            }
        }


    }

    void StartGame()
    {
        AirConsole.instance.SetActivePlayers(2);
    }

    void OnDisconnect(int device_id)
    {
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (active_player != -1)
        {
            if (AirConsole.instance.GetControllerDeviceIds().Count >= 2)
            {
                StartGame();
            }
            else
            {
                AirConsole.instance.SetActivePlayers(0);
            }
        }
    }

    public void InputPressed(int from, JToken data) {
        Debug.Log("Received message: " + data);

        //When I get a message, I check if it's from any of the devices stored in my device Id dictionary
        if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 2)
        {
            int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
            if (active_player != -1)
            {
                if (active_player == 0)
                {
                    // do stuff with smithBot;
                }
                if (active_player == 1)
                {
                    // do stuff with sparkyBot;
                }
            }

            PlayerBot playerBot = smithBot;

            if (data["joystick-left"] != null)
            {
                playerBot.ControlJoystickToggle(GameConstants.JoystickControlMessage.MoveJoystick, (bool) data["joystick-left"]["pressed"]);
                if((bool)data["joystick-left"]["pressed"])
                {
                    playerBot.ControlJoystickInput(GameConstants.JoystickControlMessage.MoveJoystick, (float)data["joystick-left"]["message"]["x"], (float)data["joystick-left"]["message"]["y"]);
                }
            } else if(data["action1"] != null)
            {
                playerBot.ControlButton(GameConstants.ButtonMessage.Special1Pressed, (bool)data["action1"]["pressed"]);
            }
            else if (data["action2"] != null)
            {
                playerBot.ControlButton(GameConstants.ButtonMessage.Special2Pressed, (bool)data["action2"]["pressed"]);
            }
            else if (data["action3"] != null)
            {
                playerBot.ControlButton(GameConstants.ButtonMessage.SprintPressed, (bool)data["action3"]["pressed"]);
            }
            //I forward the command to the r0levant player script, assigned by device ID
        }




    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
