using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinigamePageUI : MonoBehaviour {
    public string minigame;
    Image minigameThumbnail;
    public Button playButton;

    void Update()
    {
        if (MasterGameManager.instance.workboard.currentWorkOrders.Count > 0)
            playButton.interactable = true;
        else
            playButton.interactable = false;
    }

    public void DisplayPage(string minigame)
    {
        MasterGameManager.instance.workboard.currentWorkOrders.Clear();
        SetMinigame(minigame);
        this.transform.parent.gameObject.SetActive(true);
    }

    void SetMinigame(string minigame)
    {
        this.minigame = minigame;
        Sprite thumbnail = Resources.Load<Sprite>(string.Format("MinigameThumbnails/{0}Thumbnail", minigame));
        this.transform.Find("MinigameThumb").GetComponent<Image>().sprite = thumbnail;
    }

    public void PlayMinigame()
    {
        if (!MasterGameManager.instance.sceneManager.loadingScene)
        {
            MasterGameManager.instance.actionClock.SpendAction();
            MasterGameManager.instance.sceneManager.LoadScene(minigame);
        }
    }
}
