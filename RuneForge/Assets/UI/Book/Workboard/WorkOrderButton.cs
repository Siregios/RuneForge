using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    Button button;
    bool selected;  // For minigame page
    public Text orderName;
    public Image orderIcon;
    public Text stageText;
    public RectTransform gauge;
    float gaugeMaxWidth;
    public Text scoreText;

    void Awake()
    {
        button = this.GetComponent<Button>();
        gaugeMaxWidth = gauge.rect.width;
    }

    public void Initialize(WorkOrder order)
    {
        this.order = order;
        this.orderName.text = order.item.name;
        this.orderIcon.sprite = order.item.icon;
        this.stageText.text = string.Format("{0}/{1}", order.currentStage, order.requiredStages);
        this.gauge.sizeDelta = new Vector2(gaugeMaxWidth * ((float)order.currentStage / order.requiredStages), gauge.rect.height);
        this.scoreText.text = order.score.ToString();

        if (order.isRandom)
        {
            this.GetComponent<Image>().color = Color.yellow;
        }

        switch (uiType)
        {
            case UIType.Workboard:
                workOrderPanel = GameObject.Find("WorkOrderPanel").GetComponent<WorkOrderPageUI>();
                break;
            case UIType.Minigame:
                minigamePanel = GameObject.Find("MinigamePage").GetComponent<MinigamePageUI>();

                string minigame = minigamePanel.minigame;
                if (!this.order.isRandom && this.order.MinigameListContains(minigame))
                {
                    this.GetComponent<Button>().interactable = false;
                }
                else if (this.order.isRandom && !this.order.MinigameAt(minigame, this.order.currentStage))
                {
                    this.GetComponent<Button>().interactable = false;
                }
                break;
        }
    }
    
    void Update()
    {
        WorkOrderManager orderManager = MasterGameManager.instance.workOrderManager;
        if (orderManager.currentWorkOrders.Contains(this.order))
        {
            this.button.targetGraphic.color = this.button.colors.pressedColor;
            selected = true;
        }
        else
        {
            this.button.targetGraphic.color = this.button.colors.normalColor;
            selected = false;
        }
    }

    public void WorkOrderClick()
    {
        workOrderPanel.LoadOrder(this.order);
    }

    public void MinigameClick()
    {
        WorkOrderManager workOrderManager = MasterGameManager.instance.workOrderManager;
        if (!selected)
        {
            /// This prevents working on multiple orders at once. We should wrap this in an if so that it stops when the player
            /// has the upgrade to work on multiples
            workOrderManager.currentWorkOrders.Clear();

            workOrderManager.WorkOnOrder(this.order);
        }
    }
}
