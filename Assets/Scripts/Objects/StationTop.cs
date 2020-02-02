using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTop : MonoBehaviour
{
    public GameConstants.StationType stationType;
    public bool isWorking;
    public float progress;
    public float speed;
    public ProductBox currentBox;
    public Transform boxTransform;

    public GameObject progressBarPrefab;
    private StationProgressBar progressBar;
    private Canvas canvas;
    private RectTransform canvasRect;
    public float progressBarOffsetZ;
    public Transform workTransform;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        GameObject progressBarGameObject = Instantiate(progressBarPrefab, canvas.transform);
        progressBar = progressBarGameObject.GetComponent<StationProgressBar>();
        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + progressBarOffsetZ);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Set
        progressBar.transform.localPosition = canvasPos;
    }


    void BoxDone()
    {
        currentBox.ProcessDone(stationType);
        isWorking = false;
    }

    public void SetCurrentBox(ProductBox incomingBox)
    {
        currentBox = incomingBox;
        // make the box place on the top of station

        incomingBox.MoveBoxTo(boxTransform);

        currentBox.transform.position = boxTransform.position;
    }

    void CurrBoxPickedUp(PlayerBot player)
    {
        player.GetBox(currentBox);
        currentBox = null;
        Debug.Log("Got!");
    }

    // Update is called once per frame
    void Update()
    {

        if (isWorking)
        {
            
            progress += speed * Time.deltaTime;
            if (progressBar)
            {
                progressBar.setStatusBarMode(ProgressBarMode.WITHBG);
                progressBar.updateBar(progress, 1f);
            }
            if (progress >= 1f)
            {
                BoxDone();
            }
        }
        else
        {
            // so the progressbar will show only when isWorking is true
            if (progressBar)
            {
                progressBar.setStatusBarMode(ProgressBarMode.HIDDEN);
            }
        }

        if (currentBox)
        {
            // make the box place on the top of station
            currentBox.transform.position = boxTransform.position;
        }
    }

    public void Interact(PlayerBot player, ProductBox incomingBox)
    {
        Debug.Log("Station Top Interacted " + gameObject.name);

        if (stationType == GameConstants.StationType.CTable)
        {
            progress = 1f;
        }

        if (incomingBox && !currentBox)
        {
            Debug.Log("Placed!");
            SetCurrentBox(incomingBox);
            player.BoxGiven();
            player.StartWork(this);
            if (incomingBox.currentWork < incomingBox.processes.Count && incomingBox.processes[incomingBox.currentWork] == stationType && PlayerTypeCompatibleCheck(player))
            {
                isWorking = true;
                progress = 0f;
            }
        }
        else if(!incomingBox && currentBox)
        {
            if (!isWorking && progress >= 1f)
            {
                if(progress >= 1f)
                {
                    CurrBoxPickedUp(player);
                } else
                {
                    isWorking = true;
                    player.StartWork(this);
                }
            }
        }

        if (stationType == GameConstants.StationType.CBoxGenerator)
        {
            this.GetComponent<BoxGenerator>().SpawnBox();
        }
    }

    public void Pause()
    {
        isWorking = false;
    }

    private bool PlayerTypeCompatibleCheck(PlayerBot player)
    {
        switch(stationType)
        {
            case GameConstants.StationType.HWelding:
            case GameConstants.StationType.HWiring:
                return player.botType == GameConstants.PlayerBotType.Heavy;
                break;
            case GameConstants.StationType.LElectric:
            case GameConstants.StationType.LProgramming:
                return player.botType == GameConstants.PlayerBotType.Light;
                break;
            case GameConstants.StationType.CInspection:
            case GameConstants.StationType.CPolishing:
            case GameConstants.StationType.CTable:
                return true;
            default:
                return false;
        }
    }
}
