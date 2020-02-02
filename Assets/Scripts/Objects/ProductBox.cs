using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBox : MonoBehaviour
{
    public int boxId;
    public BoxPanel boxPanel = null;

    public List<GameConstants.StationType> processes;
    public int currentWork;
    public bool inspected;
    //public float timeRemainingToRepair;
    //public float timeToRepair;

    public bool isCarried;
    public bool isPlaced;

    public Sequence movementTween;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveBoxTo(Transform targetTrans)
    {
        if(movementTween != null)
        {
            movementTween.Kill(true);
        }


        // DoTweenAnimation Here

        movementTween = DOTween.Sequence();
        transform.DOMove(targetTrans.position, 1.0f).SetEase(Ease.InOutBack);
        transform.DORotate(targetTrans.rotation.eulerAngles, 1.0f).SetEase(Ease.InOutBack);
    }

    public void ProcessDone(GameConstants.StationType workType)
    {
        currentWork++;
        inspected = true;
        if(boxPanel)
        {
            if (workType == GameConstants.StationType.CInspection)
            {
                boxPanel.showAllProductionIcons();
                // remove question mark
            }
            boxPanel.updateUI(processes, currentWork, inspected);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //timeRemainingToRepair -= Time.deltaTime;
        //if (timeRemainingToRepair <= 0f)
        //{
        //    // TODO:
        //}
    }

    public GameConstants.StationType GetCurrentJob()
    {
        return processes[currentWork];
    }
}
