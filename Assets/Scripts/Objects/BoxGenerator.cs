using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    // TODO: The first process MUST BE inspecting
    public int maxProcessesPerBox; // this should be other process +1 because the first one must be inspecting
    public List<string> processTypeSequences;
    public List<GameConstants.StationType> heavyProcessToBeRandomize;
    public List<GameConstants.StationType> lightProcessToBeRandomize;
    public List<GameConstants.StationType> commonProcessToBeRandomize;

    public List<GameObject> boxesPool;
    public GameObject boxPrefab;

    private StationTop thisStation;
    private int releasingBoxIndex;

    // Start is called before the first frame update
    void Start()
    {
        thisStation = GetComponent<StationTop>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (isGameStarted)
        {
            if (GameManager.currentBox < GameManager.maxBoxes)
            {
                GameObject newBox = getNewObjectFromPool();
                ProductBox productBox = newBox.GetComponent<ProductBox>();

            }
        }*/

        // no box currently on generator
        if(!thisStation.currentBox)
        {
            // pull out a new box
        }
    }

    public void instantiatePool(int numsOfBoxes)
    {
        for(int i = 0; i < numsOfBoxes; i++)
        {
            GameObject currentBoxGameObject = Instantiate(boxPrefab);
            boxesPool.Add(currentBoxGameObject);
            ProductBox productBox = currentBoxGameObject.GetComponent<ProductBox>();
            int randomIndex = Random.Range(0, processTypeSequences.Count);
            while (processTypeSequences[randomIndex].Length > maxProcessesPerBox)
            {
                randomIndex = Random.Range(0, processTypeSequences.Count);
            }

            productBox.processes.Add(GameConstants.StationType.CInspection);
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
            }
            productBox.processes.Add(GameConstants.StationType.CPolishing);
        }
    }
}
