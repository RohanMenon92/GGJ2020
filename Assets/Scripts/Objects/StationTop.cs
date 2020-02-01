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
        if (isWorking)
        {
            progress += speed * Time.deltaTime;
            if (progress >= 1f)
            {
                currentBox.currentWork++;
                currentBox.inspected = true;
                isWorking = false;
            }
        }

        if(currentBox)
        {
            // make the box place on the top of station
            currentBox.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        }
    }

    public void Interact(PlayerBot player, ProductBox box)
    {
        if (box && !currentBox)
        {
            currentBox = box;
            player.carryingBox = null;
            if (box.currentWork < box.processes.Count && box.processes[box.currentWork] == stationType && PlayerTypeCompatibleCheck(player))
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

    public void Pause()
    {
        isWorking = false;
    }

    private bool PlayerTypeCompatibleCheck(PlayerBot player)
    {
        switch(stationType)
        {
            case GameConstants.StationType.HWelding:
            case GameConstants.StationType.HWiring:
                return player.botType == GameConstants.PlayerBotType.Heavy;
                break;
            case GameConstants.StationType.LElectric:
            case GameConstants.StationType.LProgramming:
                return player.botType == GameConstants.PlayerBotType.Light;
                break;
            case GameConstants.StationType.CInspection:
            case GameConstants.StationType.CPolishing:
            case GameConstants.StationType.CTable:
                return true;
            default:
                return false;
        }
    }
}
