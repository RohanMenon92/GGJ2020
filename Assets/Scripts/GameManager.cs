using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject resultsPanel;
    public GameObject mainMenuPanel;
    public Transform howToPlayPanel;
    public Transform readyText;
    public Transform disconnectText;

    public int numsOfBoxes;
    public float levelTime;
    public bool isPlaying;

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
        boxGenerator.instantiatePool(numsOfBoxes);
        //startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            levelTime -= Time.deltaTime;
        }
    }

    public void startGame()
    {
        isPlaying = true;
        boxGenerator.SpawnBox();
        AirConsole.instance.SetActivePlayers(2);
    }

    public PlayerBot OnPlayerAdded() {
        // TODO Return unconnected player here
        return FindObjectOfType<PlayerBot>();
    }

    public void showResult()
    {
        isPlaying = false;
        resultsPanel.SetActive(true);
    }
}
