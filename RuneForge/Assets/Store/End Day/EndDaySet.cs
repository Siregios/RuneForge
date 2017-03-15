﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDaySet : MonoBehaviour {

    public Text level;
    public Text exp;
    public Text gold;
    public Text action;
    public Text quest;
    public GameObject calendar;
    public Image fader;
    bool corRun = true;
    bool noExit = true;

    void Start()
    {
        MasterGameManager.instance.uiManager.uiOpen = true;

        StoreDayStats sdt = MasterGameManager.instance.storeDayStats;
        PlayerStats pt = MasterGameManager.instance.playerStats;
        level.text = "Levels Gained: " + (pt.level - sdt.level).ToString();
        exp.text = "EXP Gained: " + (pt.currentExperience - sdt.experience).ToString();
        int money = PlayerInventory.money - sdt.money;
        if (money < 0)
            money = 0;
        gold.text = "Gold Gained: " + money.ToString();
        action.text = "Actions Used: " + (sdt.actions - MasterGameManager.instance.actionClock.ActionCount).ToString();
        quest.text = "Customers Helped: " + sdt.quests;
        MasterGameManager.instance.actionClock.ActionCount = MasterGameManager.instance.actionClock.ActionsPerDay;
        calendar.GetComponent<CalendarPage>().DisplayPage();
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !corRun && !noExit)
        {
            MasterGameManager.instance.storeDayStats.copyGameStats();
            MasterGameManager.instance.sceneManager.LoadScene("Store");
        }
    }
    IEnumerator FadeIn()
    {
        while (fader.fillAmount > 0)
        {
            fader.fillAmount -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        corRun = false;
    }

    public void setExitBool(bool set)
    {
        noExit = set;
    }
}
