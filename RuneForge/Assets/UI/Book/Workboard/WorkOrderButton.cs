using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkOrderButton : MonoBehaviour {
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
        this.orderNumber.text = order.orderNumber.ToString();
        this.orderIcon.sprite = order.item.icon;
        if (order.isRandom)
        {
            this.GetComponent<Image>().color = Color.yellow;
        }
    }
}
