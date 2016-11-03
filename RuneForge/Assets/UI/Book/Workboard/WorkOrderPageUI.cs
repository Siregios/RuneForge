using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkOrderPageUI : MonoBehaviour { 
    public GameObject minigameLine; //A panel with a check icon (Image), name of the minigame (Text), and score (Text)
    WorkOrder order;
    Text orderText;
    Image orderIcon;

    void Awake()
    {
        this.orderText = this.transform.Find("OrderText").GetComponent<Text>();
        this.orderIcon = this.transform.Find("IconPanel/Icon").GetComponent<Image>();
    }

    public void LoadOrder(WorkOrder order)
    {
        this.order = order;
        orderText.text = string.Format("{0}) {1}", order.orderNumber, order.item.name);
        orderIcon.sprite = order.item.icon;
    }
}