using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    public enum StationType {
        LWelding,
        LElectric,
        LProgramming,
        HDisassembly,
        HWiring,
        HBuffing,
        CPolishing,
        CTable,
        CInspection,
        CBoxGenerator,
        CBoxSlot
    }

    public enum PlayerBotType
    {
        Light,
        Heavy
    }
    
    public enum ActionMessage
    {
        JoyStickPressed, // Will handle input of 2 x, y values
        Special1Pressed, // Can Handle sensitivity later
        Special2Pressed, // same
        SprintPressed // Regular true or flase value
    }


}
