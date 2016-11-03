using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkOrderButton : MonoBehaviour {
    public enum UIType
    {
        Workboard,
        Minigame
    }
    public UIType uiType;
    WorkOrderPageUI workOrderPanel;
    MinigamePageUI minigamePanel;

    WorkOrder order;
    Text orderNumber;
    Image orderIcon;
    GameObject stagePanel, scorePanel;

    void Awake()
    {
        this.orderNumber = this.transform.Find("OrderNumber").GetComponent<Text>();
        this.orderIcon = this.transform.Find("OrderIcon").GetComponent<Image>();
        this.stagePanel = this.transform.Find("StagePanel").gameObject;
        this.scorePanel = this.transform.Find("ScorePanel").gameObject;
    }

    public void Initialize(WorkOrder order)
    {
        this.order = order;
        this.orderNumber.text = order.orderNumber.ToString();
        this.orderIcon.sprite = order.item.icon;
        if (order.isRandom)
        {
            this.GetComponent<Image>().color = Color.yellow;
        }

        switch (uiType)
        {
            case UIType.Workboard:
                workOrderPanel = this.transform.root.Find("Book - Workboard/WorkOrderPanel").GetComponent<WorkOrderPageUI>();
                break;
            case UIType.Minigame:
                minigamePanel = this.transform.root.Find("Book - Minigame/MinigamePanel").GetComponent<MinigamePageUI>();
                break;
        }
    }

    public void WorkOrderClick()
    {
        workOrderPanel.LoadOrder(this.order);
    }

    public void MinigameClick()
    {

    }
}
