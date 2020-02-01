using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class UserControl : MonoBehaviour
{
    public Dictionary<int, PlayerBot> devicesConnected = new Dictionary<int, PlayerBot>();

    // Private
    GameManager gameManager;


    // Start is called before the first frame update
    void Awake()
    {
        devicesConnected.Clear();
        gameManager = FindObjectOfType<GameManager>();
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += AddNewPlayer;
        AirConsole.instance.onMessage += InputPressed;
    }

    void OnReady(string code)
    {
        Debug.Log("On ready called with code " + code);
    }

    public void AddNewPlayer(int deviceID)
    {
        Debug.Log("New Player Added " + deviceID);

        if (devicesConnected.ContainsKey(deviceID))
        {
            return;
        }

        PlayerBot newPlayerBot = gameManager.OnPlayerAdded();

        devicesConnected.Add(deviceID, newPlayerBot);

    }

    public void InputPressed(int from, JToken data) {
        Debug.Log("Received message: " + data);

        //When I get a message, I check if it's from any of the devices stored in my device Id dictionary
        if (devicesConnected.ContainsKey(from))
        {
            PlayerBot playerBot = devicesConnected[from];

            if (data["joystick-left"] != null)
            {
                playerBot.ControlJoystickToggle(GameConstants.JoystickControlMessage.MoveJoystick, (bool) data["joystick-left"]["message"]["pressed"]);
                playerBot.ControlJoystickInput(GameConstants.JoystickControlMessage.MoveJoystick, (float)data["joystick-left"]["message"]["x"], (float)data["joystick-left"]["message"]["y"]);
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
