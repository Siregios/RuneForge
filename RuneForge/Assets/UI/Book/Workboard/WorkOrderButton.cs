﻿using UnityEngine;
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
    ClipboardUI clipboard;
    WorkOrderPageUI workOrderPanel;
    MinigamePageUI minigamePanel;

    WorkOrder order;
    Button button;
    bool selected = false;  // For minigame page
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

    public void Initialize(WorkOrder order, ClipboardUI clipboard)
    {
        this.clipboard = clipboard;
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

                if (!this.order.CanPlayMinigame(minigame))
                    this.button.interactable = false;

                break;
        }
    }
    
    void Update()
    {
        WorkOrderManager orderManager = MasterGameManager.instance.workOrderManager;
        if (uiType == UIType.Minigame)
        {
            if (orderManager.currentWorkOrders.Contains(this.order))
            {
                selected = true;
            }
            else
            {
                selected = false;
            }
        }

        if (selected)
            SelectedColors();
        else
            DeselectedColors();
    }

    public void WorkOrderClick()
    {
        _Click();
        workOrderPanel.LoadOrder(this.order);
    }

    public void MinigameClick()
    {
        _Click();
        WorkOrderManager workOrderManager = MasterGameManager.instance.workOrderManager;
        //if (!selected)
        //{
            /// This prevents working on multiple orders at once. We should wrap this in an if so that it stops when the player
            /// has the upgrade to work on multiples
            workOrderManager.currentWorkOrders.Clear();

            workOrderManager.WorkOnOrder(this.order);
        //}
    }

    void _Click()
    {
        foreach (WorkOrderButton button in clipboard.buttonList)
        {
            button.selected = false;
        }
        this.selected = true;
    }

    void SelectedColors()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = Color.white;
        cb.pressedColor = Color.white;
        button.colors = cb;
    }

    void DeselectedColors()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = new Color(.6f, .6f, .6f);
        cb.highlightedColor = new Color(.8f, .8f, 8f);
        cb.pressedColor = Color.white;
        button.colors = cb;
    }
}
