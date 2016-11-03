using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinigamePageUI : MonoBehaviour {
    string minigame;
    Image minigameThumbnail;

    void Awake()
    {
        minigameThumbnail = this.transform.Find("MinigameThumb").GetComponent<Image>();
    }

    public void DisplayPage(string minigame)
    {
        SetMinigame(minigame);
    }

    void SetMinigame(string minigame)
    {
        this.minigame = minigame;
        Sprite thumbnail = Resources.Load<Sprite>(string.Format("MinigameThumbnails/{0}Thumbnail", minigame));
        minigameThumbnail.sprite = thumbnail;
    }

    public void PlayMinigame()
    {
        MasterGameManager.instance.sceneManager.LoadScene(minigame);
    }
}
