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
    public Transform boxTransform;
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
                currentBox.ProcessDone(stationType);
                isWorking = false;
            }
        }

        if(currentBox)
        {
            // make the box place on the top of station
            currentBox.transform.position = boxTransform.position;
        }
    }

    public void Interact(PlayerBot player, ProductBox incomingBox)
    {
        Debug.Log("Station Top Interacted " + gameObject.name);

        if (stationType == GameConstants.StationType.CTable)
        {
            progress = 1f;
        }

        if (incomingBox && !currentBox)
        {
            Debug.Log("Placed!");
            currentBox = incomingBox;
            player.carryingBox = null;
            if (incomingBox.currentWork < incomingBox.processes.Count && incomingBox.processes[incomingBox.currentWork] == stationType && PlayerTypeCompatibleCheck(player))
            {
                isWorking = true;
                progress = 0f;
            }
        }
        else if(!incomingBox && currentBox)
        {
            
            if (!isWorking && progress >= 1f)
            {
                player.carryingBox = currentBox;
                currentBox = null;
                Debug.Log("Got!");
            }
            //isWorking = true;
        }

        if (stationType == GameConstants.StationType.CBoxGenerator)
        {
            this.GetComponent<BoxGenerator>().SpawnBox();
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
