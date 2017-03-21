﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class QuestNote : MonoBehaviour {
    public GameObject eventSystem;
    public Quest quest;
    private bool PlayOnce;
    public AudioClip clickSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Image>().sprite == gameObject.transform.Find("QuestIcon").GetComponent<Image>().sprite)
        {
            transform.parent.GetComponent<QuestBoardUI>().turnInQuest(gameObject, other.gameObject.transform.parent.gameObject);
            other.transform.parent.GetComponent<ItemButton>().OnMouseRelease();
        }
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            GetComponent<Button>().image.color = Color.cyan;
            if(PlayOnce)
            {
                MasterGameManager.instance.audioManager.PlaySFXClip(clickSound);
                PlayOnce = false;
            }
        }

        else
        {
            GetComponent<Button>().image.color = Color.white;
            PlayOnce = true;
        }
    }
    //bool followingMouse = false;

    //void Update()
    //{
    //    if (followingMouse)
    //        this.transform.position = Input.mousePosition;
    //}

    //public void OnMouseDown()
    //{
    //    followingMouse = true;
    //}

    //public void OnMouseRelease()
    //{
    //    followingMouse = false;
    //}
}
