﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuBar : MonoBehaviour {
    Text moneyText;
    Text dayText;
    Text actionText;

    void Awake()
    {
        moneyText = this.transform.FindChild("MoneyPanel/MoneyText").GetComponent<Text>();
        dayText = this.transform.FindChild("DayText").GetComponent<Text>();
        actionText = this.transform.FindChild("ActionText").GetComponent<Text>();
    }

    void Update()
    {
        //moneyText.text = PlayerInventory.inventory.GetItemCount("Money").ToString();
        moneyText.text = PlayerInventory.money.ToString();
        dayText.text = "Day: " + MasterGameManager.instance.actionClock.Day;

        actionText.text = string.Format("Actions: {0}/{1}", MasterGameManager.instance.actionClock.ActionCount, MasterGameManager.instance.actionClock.ActionsPerDay);
    }
}
