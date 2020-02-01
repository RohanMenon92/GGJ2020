using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTop : MonoBehaviour
{
    public GameConstants.StationType stationType;
    public bool isWorking;
    public float progress;
    public float speed;
    public ProductBox currentBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking && progress >= 1f)
        {
            progress += speed * Time.deltaTime;
            currentBox.doneIndex++;
            isWorking = false;
        }
    }

    public void Interact(PlayerBot player, ProductBox box)
    {
        if (box && !currentBox)
        {
            currentBox = box;
            player.carryingBox = null;
            if (box.doneIndex < box.processes.Count && box.processes[box.doneIndex] == stationType && PlayerTypeCompatibleCheck(player))
            {
                isWorking = true;
                progress = 0f;
            }

            if (!isWorking && progress >= 1f)
            {
                player.carryingBox = currentBox;
                currentBox = null;
            }
        }
        else if(currentBox)
        {
            isWorking = true;
        }
    }

    public void Cancel()
    {
        isWorking = false;
    }

    private bool PlayerTypeCompatibleCheck(PlayerBot player)
    {
        if (player.botType == GameConstants.PlayerBotType.Heavy)
        {
            switch(stationType)
            {
                case GameConstants.StationType.HBuffing:
                case GameConstants.StationType.HDisassembly:
                case GameConstants.StationType.HWiring:
                case GameConstants.StationType.CInspection:
                case GameConstants.StationType.CPolishing:
                case GameConstants.StationType.CTable:
                    return true;
                default:
                    return false;

            }
        }
        else if(player.botType == GameConstants.PlayerBotType.Light)
        {
            switch (stationType)
            {
                case GameConstants.StationType.LElectric:
                case GameConstants.StationType.LProgramming:
                case GameConstants.StationType.LWelding:
                case GameConstants.StationType.CInspection:
                case GameConstants.StationType.CPolishing:
                case GameConstants.StationType.CTable:
                    return true;
                default:
                    return false;

            }
        }
        else
        {
            return false;
        }
    }
}
