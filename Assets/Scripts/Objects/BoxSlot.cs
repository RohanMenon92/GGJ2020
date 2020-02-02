using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSlot : MonoBehaviour
{
    public int numOfBoxesAccepted = 0;

    private StationTop thisStation;
    private GameManager gManager;
    // Start is called before the first frame update
    void Start()
    {
        thisStation = GetComponent<StationTop>();
        gManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisStation.currentBox)
        {
            ProductBox currentBox = thisStation.currentBox;
            if(currentBox.processes.Count == currentBox.currentWork)
            {
                // the box is done
                numOfBoxesAccepted++;
                // play animation

            }
        }

        if (numOfBoxesAccepted == gManager.numsOfBoxes)
        {
            gManager.showResult();
        }
    }
}
