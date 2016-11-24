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
    public Text orderName;
    public Image orderIcon;
    public Text stageText;
    public RectTransform gauge;
    float gaugeMaxWidth;
    public Text scoreText;

    void Awake()
    {
        workOrderPanel = this.transform.parent.GetComponent<WorkOrderPageUI>();
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
                workOrderPanel = this.transform.root.Find("Book - Workboard/WorkOrderPanel").GetComponent<WorkOrderPageUI>();
                break;
            case UIType.Minigame:
                minigamePanel = this.transform.root.Find("Book - Minigame/MinigamePanel").GetComponent<MinigamePageUI>();

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

    

    public void WorkOrderClick()
    {
        workOrderPanel.LoadOrder(this.order);
    }

    public void MinigameClick()
    {
        MasterGameManager.instance.workboard.WorkOnOrder(this.order);
        this.GetComponent<Button>().interactable = false;
    }
}
