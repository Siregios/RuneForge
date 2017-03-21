using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WorkOrderPageUI : MonoBehaviour {
    public GameObject minigameLine; //A panel with a check icon (Image), name of the minigame (Text), and score (Text)
    List<GameObject> minigameLineList = new List<GameObject>();
    WorkOrder order;
    Text orderText;
    Image orderIcon;
    GameObject minigamePanel;

    void Awake()
    {
        this.orderText = this.transform.Find("OrderText").GetComponent<Text>();
        this.orderIcon = this.transform.Find("IconPanel/Icon").GetComponent<Image>();
        this.minigamePanel = this.transform.Find("MinigameList").gameObject;
    }

    public void LoadOrder(WorkOrder order)
    {
        this.order = order;
        orderText.text = string.Format("{0}) {1}", order.orderNumber, order.item.name);
        orderIcon.sprite = order.item.icon;

        ClearMinigameLineList();

        for (int i = 0; i < this.order.requiredStages; i++)
        {
            CreateNewMinigameLine(i);
        }
    }

    void CreateNewMinigameLine(int index)
    {
        GameObject newMinigameLine = (GameObject)Instantiate(minigameLine, minigamePanel.transform.position, Quaternion.identity);
        newMinigameLine.transform.SetParent(minigamePanel.transform);
        newMinigameLine.transform.localScale = Vector3.one;
        newMinigameLine.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, index * -30, 0);

        //Show the check if this minigame has been completed
        Image check = newMinigameLine.transform.FindChild("CheckIcon").GetComponent<Image>();
        Text minigameName = newMinigameLine.transform.FindChild("MinigameName").GetComponent<Text>();
        Text minigameScore = newMinigameLine.transform.FindChild("MinigameScore").GetComponent<Text>();
        if (this.order.currentStage > index)
        {
            check.gameObject.SetActive(true);
            minigameName.text = this.order.minigameList[index].Key;
            minigameScore.text = this.order.minigameList[index].Value.ToString();
        }
        else
        {
            check.gameObject.SetActive(false);
            if (this.order.isRandom)
                minigameName.text = this.order.minigameList[index].Key;
            else
                minigameScore.text = "";
            minigameScore.text = "";
        }

        minigameLineList.Add(newMinigameLine);
    }

    void ClearMinigameLineList()
    {
        foreach (GameObject minigameLine in minigameLineList)
        {
            Destroy(minigameLine);
        }

        minigameLineList.Clear();
    }
}