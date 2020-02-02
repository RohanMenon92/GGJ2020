using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxGenerator : MonoBehaviour
{
    // TODO: The first process MUST BE inspecting
    public int maxProcessesPerBox; // this should be other process +1 because the first one must be inspecting
    public List<string> processTypeSequences;
    public List<GameConstants.StationType> heavyProcessToBeRandomize;
    public List<GameConstants.StationType> lightProcessToBeRandomize;
    public List<GameConstants.StationType> commonProcessToBeRandomize;

    public List<ProductBox> boxesPool;
    public GameObject boxPrefab;
    public GameObject boxPanelPrefab;
    public GameObject productionIconsPrefab;
    public Transform boxPanels;

    private StationTop thisStation;
    private int releasingBoxIndex;

    // Start is called before the first frame update
    void Start()
    {
        thisStation = GetComponent<StationTop>();
        instantiatePool(3);

    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void SpawnBox()
    {
        // no box currently on generator
        if (!thisStation.currentBox && releasingBoxIndex < boxesPool.Count) // releasingBoxIndex < numsOfBoxInLevel
        {
            // pull out a new box
            // do whatever else needs to be done
            MoveBoxToTop();
            thisStation.SetCurrentBox(boxesPool[releasingBoxIndex]);
            thisStation.isWorking = false;
            thisStation.progress = 1f;
            releasingBoxIndex++;
        }
    }

    public void MoveBoxToTop()
    {
        boxesPool[releasingBoxIndex].transform.position = 
            new Vector3(thisStation.boxTransform.position.x, thisStation.boxTransform.position.y - 2f, thisStation.boxTransform.position.z);
    }



    public void instantiatePool(int numsOfBoxes)
    {
        for(int i = 0; i < numsOfBoxes; i++)
        {
            //Instatiate Boxpanel
            GameObject boxPanelGameObject = Instantiate(boxPanelPrefab, boxPanels);
            GameObject currentBoxGameObject = Instantiate(boxPrefab);
            ProductBox productBox = currentBoxGameObject.GetComponent<ProductBox>();
            BoxPanel boxPanel = boxPanelGameObject.GetComponent<BoxPanel>();
            productBox.boxPanel = boxPanel;
            boxesPool.Add(productBox);
            int randomIndex = Random.Range(0, processTypeSequences.Count);
            while (processTypeSequences[randomIndex].Length > maxProcessesPerBox)
            {
                randomIndex = Random.Range(0, processTypeSequences.Count);
            }

            productBox.processes.Add(GameConstants.StationType.CInspection);
            Instantiate(productionIconsPrefab, boxPanelGameObject.GetComponent<BoxPanel>().productionIconsTransform.transform);

            for (int seqIndex = 0; seqIndex < processTypeSequences[randomIndex].Length; seqIndex++)
            {
                switch (processTypeSequences[randomIndex][seqIndex])
                {
                    case 'L':
                        productBox.processes.Add(lightProcessToBeRandomize[Random.Range(0, lightProcessToBeRandomize.Count)]);
                        break;
                    case 'H':
                        productBox.processes.Add(heavyProcessToBeRandomize[Random.Range(0, heavyProcessToBeRandomize.Count)]);
                        break;
                    default:
                        break;
                }

                //                Instantiate(prefabIcon, transform) with parent as boxpanel;
                GameObject productionIcon = Instantiate(productionIconsPrefab, boxPanel.productionIconsTransform.transform);
                boxPanel.productionIcons.Add(productionIcon.GetComponent<Image>());



            }
            productBox.processes.Add(GameConstants.StationType.CPolishing);
            Instantiate(productionIconsPrefab, boxPanelGameObject.GetComponent<BoxPanel>().productionIconsTransform.transform);


        }
    }
}
