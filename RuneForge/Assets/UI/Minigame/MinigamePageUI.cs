using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinigamePageUI : MonoBehaviour {
    public ClipboardUI clipboard;
    public List<GameObject> recipeShortcuts;
    public string minigame;
    public Button playButton;
    public Image minigameThumbnail;
    public Animator animator;

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
        if (MasterGameManager.instance.workOrderManager.currentWorkOrders.Count > 0)
            playButton.interactable = true;
        else
            playButton.interactable = false;
    }

    public void DisplayPage(string minigame)
    {
        WorkOrderManager orderManager = MasterGameManager.instance.workOrderManager;
        orderManager.currentWorkOrders.Clear();
        if (orderManager.workorderList.Count >= 1)
        {
            MasterGameManager.instance.workOrderManager.WorkOnOrder(orderManager.workorderList[0]);
        }

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
        SetMinigame(minigame);
        Enable(true);
        animator.SetBool(minigame, true);
    }

    void SetMinigame(string minigame)
    {
        this.minigame = minigame;
        Sprite thumbnail = Resources.Load<Sprite>(string.Format("MinigameThumbnails/{0}Thumbnail", minigame));
        minigameThumbnail.sprite = thumbnail;
    }

    public void PlayMinigame()
    {
        Debug.Log(this.minigame);
        if (!MasterGameManager.instance.sceneManager.loadingScene)
        {
            MasterGameManager.instance.actionClock.SpendAction();
            MasterGameManager.instance.sceneManager.LoadScene(minigame);
        }
    }
}
