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

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void instantiatePool(int numsOfBoxes)
    {
        for(int i = 0; i < numsOfBoxes; i++)
        {
            boxesPool.Add(Instantiate(boxPrefab));
        }
    }
}
