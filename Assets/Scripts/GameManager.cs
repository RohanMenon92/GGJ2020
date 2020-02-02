using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject resultsPanel;
    public Transform howToPlayPanel;
    public Transform readyText;
    public Transform disconnectText;

    public int numsOfBoxes;

    public PlayerBot smithBot;
    public PlayerBot sparkyBot;

    private BoxGenerator boxGenerator;

    public PlayerBot GetSmithBot()
    {
        return smithBot;
    }

    public PlayerBot GetSparkyBot()
    {
        return sparkyBot;
    }

    // Start is called before the first frame update
    void Start()
    {
        boxGenerator = FindObjectOfType<BoxGenerator>();
        startGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        boxGenerator.instantiatePool(numsOfBoxes);
        boxGenerator.SpawnBox();
    }

    public PlayerBot OnPlayerAdded() {
        // TODO Return unconnected player here
        return FindObjectOfType<PlayerBot>();
    }

    public void showResult()
    {
        resultsPanel.SetActive(true);
    }
}
