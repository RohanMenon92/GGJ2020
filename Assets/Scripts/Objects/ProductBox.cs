using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBox : MonoBehaviour
{
    public int boxId;
    public List<GameConstants.StationType> processes;
    public int currentWork;
    public bool inspected;
    public float timeToRepair;

    public bool isCarried;
    public bool isPlaced;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToRepair -= Time.deltaTime;
    }
}
