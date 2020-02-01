using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    bool WPressed, DPressed, SPressed, APressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WPressed = true;
        } else
        {
            WPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SPressed = true;
        }
        else
        {
            SPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            APressed = true;
        }
        else
        {
            APressed = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DPressed = true;
        }
        else
        {
            DPressed = false;
        }

        //if(!from+)

    }


}
