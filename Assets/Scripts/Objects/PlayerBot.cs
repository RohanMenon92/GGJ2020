using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public int controllerId;
    public ProductBox carryingBox = null;
    public int movingSpeed;
    public GameConstants.PlayerBotType botType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carryingBox)
        {
            // the box should be in front of player while carrying
            //carryingBox.transform.position = ;
        }
    }
}
