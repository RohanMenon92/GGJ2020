using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nw

public class UserControl : MonoBehaviour
{


    bool UpPressed, DownPressed, LeftPressed, RightPressed, InteractPressed;
    // Start is called before the first frame update
    void Start()
    {

    }

    void InputPressed(int from, JToken data) {
        Debug.Log("message: " + data);

        //When I get a message, I check if it's from any of the devices stored in my device Id dictionary
        if (players.ContainsKey(from) && data["action"] != null)
        {
            //I forward the command to the relevant player script, assigned by device ID
            players[from].ButtonInput(data["action"].ToString());
        }



        TestEnum foo = (TestEnum)Enum.Parse(typeof(TestEnum), incoming, true);

    }

    // Update is called once per frame
    void Update()
    {
        if(UpPressed)
        {
            
        } 
        

    }
}
