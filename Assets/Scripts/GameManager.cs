using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform resultsPanel;
    public Transform howToPlayPanel;
    public Transform readyText;
    public Transform disconnectText;

    public PlayerBot smithBot;
    public PlayerBot sparkyBot;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerBot OnPlayerAdded() {
        // TODO Return unconnected player here
        return FindObjectOfType<PlayerBot>();
    }
}
