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
        this.stagePanel.transform.Find("StageText").GetComponent<Text>().text = string.Format("{0}/{1}", order.currentStage, order.requiredStages);
        this.scorePanel.transform.Find("ScoreText").GetComponent<Text>().text = order.score.ToString();
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
