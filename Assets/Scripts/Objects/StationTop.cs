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

    public void interact(PlayerBot player, ProductBox box)
    {
        if (box)
        {
            currentBox = box;
            if (box.doneIndex < box.processes.Count && box.processes[box.doneIndex] == stationType)
            {
                isWorking = true;
                progress = 0f;
            }

            if (!isWorking)
            {
                player.carryingBox = currentBox;
                currentBox = null;
            }
        }
    }
}
