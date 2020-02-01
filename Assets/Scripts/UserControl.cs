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
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void AddNewPlayer(int deviceID)
    {
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
        if (devicesConnected.ContainsKey(from) && data["action"] != null)
        {
            PlayerBot playerBot = devicesConnected[from];

            if (data["action"].ToString() == "joystick")
            {
                if(data["position"] != null)
                {
                    playerBot.ControlJoystickInput(data["name"].ToString(), (float)data["position"]["x"], (float)data["position"]["y"]);
                } else
                {
                    playerBot.ControlJoystickToggle(data["name"].ToString(), (bool)data["touch"]);
                }
            } else
            {
                playerBot.ControlButton(data["name"].ToString(), data["value"].ToString() == "true");
            }
            //I forward the command to the r0levant player script, assigned by device ID
        }




    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
