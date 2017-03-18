using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinigamePageUI : MonoBehaviour
{
    public ClipboardUI clipboard;
    public List<GameObject> recipeShortcuts;
    public string minigame;
    public Button playButton;
    public Image minigameThumbnail;
    public Text minigameInstructions;
    public Animator animator;
    WorkOrderManager orderManager;

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        clipboard.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.Enable(this.gameObject, active);

        //MasterGameManager.instance.uiManager.uiOpen = active;
        //MasterGameManager.instance.uiManager.EnableMenuBar(!active);
        //MasterGameManager.instance.interactionManager.canInteract = !active;
    }

    void Update()
    {
        if (MasterGameManager.instance.workOrderManager.currentWorkOrders.Count > 0 &&
            MasterGameManager.instance.actionClock.ActionCount > 0)
            playButton.interactable = true;
        else
            playButton.interactable = false;
    }

    public void DisplayPage(string minigame)
    {
        SetMinigame(minigame);
        orderManager = MasterGameManager.instance.workOrderManager;

        AutoSelect();

        // Deactivate all the shortcuts before displaying the current one
        foreach (GameObject recipeShortcut in recipeShortcuts)
            recipeShortcut.SetActive(false);
        // Display the current shortcut
        if (orderManager.workorderList.Count < orderManager.maxWorkOrders)
        {
            //Tutorial purposes
            if (recipeShortcuts.Count > 0)
                recipeShortcuts[orderManager.workorderList.Count].SetActive(true);
        }
        Enable(true);
        string temp = minigame.Replace("Tutorial", "");
        animator.SetBool(temp, true);
    }

    void AutoSelect()
    {
        this.orderManager.currentWorkOrders.Clear();
        foreach (WorkOrder order in orderManager.workorderList)
        {
            if (order.CanPlayMinigame(minigame))
            {
                MasterGameManager.instance.workOrderManager.WorkOnOrder(order);
                break;
            }
        }
    }

    void SetMinigame(string minigame)
    {
        this.minigame = minigame;
        string temp = minigame.Replace("Tutorial", "");
        //Sprite thumbnail = Resources.Load<Sprite>(string.Format("MinigameThumbnails/{0}Thumbnail", minigame));
        //minigameThumbnail.sprite = thumbnail;
        TextAsset instructions = Resources.Load(string.Format("MinigameInstructions/{0}Instructions", temp), typeof(TextAsset)) as TextAsset;
        minigameInstructions.text = instructions.text;
    }

    public void PlayMinigame()
    {
        //Debug.Log(this.minigame);
        if (!MasterGameManager.instance.sceneManager.loadingScene)
        {
            if (MasterGameManager.instance.actionClock.SpendAction())
            {
                MasterGameManager.instance.sceneManager.LoadScene(minigame);
            }
            else
            {
                Debug.LogError("Cannot play minigame, not enough actions today");
            }
        }
    }

    public void PlayButtonHover(bool active)
    {
        if (active && MasterGameManager.instance.actionClock.ActionCount <= 0)
        {
            HoverInfo.Load();
            HoverInfo.instance.DisplayText(playButton.gameObject, "Not enough time actions today.");
        }
        else
        {
            if (HoverInfo.instance != null)
                HoverInfo.instance.Hide();
        }
    }
}
