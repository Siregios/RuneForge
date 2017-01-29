using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinigamePageUI : MonoBehaviour {
    public ClipboardUI clipboard;
    public string minigame;
    public Button playButton;
    public Image minigameThumbnail;

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
        MasterGameManager.instance.workOrderManager.currentWorkOrders.Clear();
        SetMinigame(minigame);
        Enable(true);
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
