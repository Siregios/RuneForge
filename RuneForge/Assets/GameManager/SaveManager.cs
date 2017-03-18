﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public void SaveData()
    {
        PlayerPrefs.SetString("@General: Scene", MasterGameManager.instance.sceneManager.currentScene);
        SavePlayerStats();
        SavePlayerInventory();
        SaveActionClock();
        SaveWorkOrders();

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        LoadPlayerStats();
        LoadPlayerInventory();
        LoadActionClock();
        LoadWorkOrders();
        if (PlayerPrefs.HasKey("@General: Scene"))
            MasterGameManager.instance.sceneManager.LoadScene(PlayerPrefs.GetString("@General: Scene"));
    }

    void SavePlayerStats()
    {
        PlayerStats playerStats = MasterGameManager.instance.playerStats;
        PlayerPrefs.SetInt("@PlayerStats: Level", playerStats.level);
        PlayerPrefs.SetInt("@PlayerStats: Experience", playerStats.currentExperience);
    }

    void LoadPlayerStats()
    {
        PlayerStats playerStats = MasterGameManager.instance.playerStats;
        if (PlayerPrefs.HasKey("@PlayerStats: Level"))
            playerStats.level = PlayerPrefs.GetInt("@PlayerStats: Level", playerStats.level);
        if (PlayerPrefs.HasKey("@PlayerStats: Experience"))
            playerStats.currentExperience = PlayerPrefs.GetInt("@PlayerStats: Experience", playerStats.currentExperience);
    }

    void SavePlayerInventory()
    {
        PlayerPrefs.SetInt("@PlayerInventory: Money", PlayerInventory.money);
        foreach (var kvp in PlayerInventory.inventory.inventoryDict)
        {
            string itemName = kvp.Key.name;
            int amount = kvp.Value;
            PlayerPrefs.SetInt("@PlayerInventory: " + itemName, amount);
        }
    }

    void LoadPlayerInventory()
    {
        if (PlayerPrefs.HasKey("@PlayerInventory: Money"))
            PlayerInventory.money = PlayerPrefs.GetInt("@PlayerInventory: Money");
        foreach (Item item in ItemCollection.itemList)
        {
            if (PlayerPrefs.HasKey("@PlayerInventory: " + item.name))
                PlayerInventory.inventory.inventoryDict[item] = PlayerPrefs.GetInt("@PlayerInventory: " + item.name);
        }
    }

    void SaveActionClock()
    {
        ActionClock actionClock = MasterGameManager.instance.actionClock;
        PlayerPrefs.SetInt("@ActionClock: ActionCount", actionClock.ActionCount);
        PlayerPrefs.SetInt("@ActionClock: ActionsPerDay", actionClock.ActionsPerDay);
        PlayerPrefs.SetInt("@ActionClock: Day", actionClock.Day);
        PlayerPrefs.SetString("@ActionClock: Season", actionClock.Season);
    }

    void LoadActionClock()
    {
        ActionClock actionClock = MasterGameManager.instance.actionClock;
        if (PlayerPrefs.HasKey("@ActionClock: ActionCount"))
            actionClock.ActionCount = PlayerPrefs.GetInt("@ActionClock: ActionCount");
        if (PlayerPrefs.HasKey("@ActionClock: ActionsPerDay"))
            actionClock.ActionsPerDay = PlayerPrefs.GetInt("@ActionClock: ActionsPerDay");
        if (PlayerPrefs.HasKey("@ActionClock: Day"))
            actionClock.Day = PlayerPrefs.GetInt("@ActionClock: Day");
        if (PlayerPrefs.HasKey("@ActionClock: Season"))
            actionClock.Season = PlayerPrefs.GetString("@ActionClock: Season");
    }

    void SaveWorkOrders()
    {
        foreach (WorkOrder order in MasterGameManager.instance.workOrderManager.workorderList)
        {
            int orderNumber = order.orderNumber;
            string itemName = order.item.name;
            int isEnhanced = Convert.ToInt32(order.isEnhanced);
            int score = order.score;
            string minigameListString = "";
            foreach (var kvp in order.minigameList)
            {
                minigameListString += string.Format("{0}:{1},", kvp.Key, kvp.Value);
            }
            if (minigameListString.Length > 0)
                minigameListString = minigameListString.Substring(0, minigameListString.Length - 1);

            string saveKey = string.Format("@WorkOrder #{0}", orderNumber);
            string saveValue = string.Format("{0}-{1}-{2}-{3}", itemName, isEnhanced, score, minigameListString);
            PlayerPrefs.SetString(saveKey, saveValue);
        }
    }

    void LoadWorkOrders()
    {
        WorkOrderManager orderManager = MasterGameManager.instance.workOrderManager;
        orderManager.workorderList.Clear();
        for (int orderNum = 1; orderNum < orderManager.maxWorkOrders + 1; orderNum++)
        {
            string saveKey = string.Format("@WorkOrder #{0}", orderNum);
            if (PlayerPrefs.HasKey(saveKey))
            {
                string saveValue = PlayerPrefs.GetString(saveKey);
                List<string> values = saveValue.Split('-').ToList();
                Item item = ItemCollection.itemDict[values[0]];
                bool isEnhanced = Convert.ToBoolean(Convert.ToInt32(values[1]));
                orderManager.CreateWorkOrder(item, isEnhanced, false);

                WorkOrder order = orderManager.workorderList.Last();
                int score = Convert.ToInt32(values[2]);
                order.score = score;

                string minigameListString = values[3];
                if (minigameListString != "")
                {
                    foreach (string kvp in minigameListString.Split(','))
                    {
                        var pair = kvp.Split(':');
                        string minigame = pair[0];
                        int minigameScore = Convert.ToInt32(pair[1]);
                        order.minigameList.Add(new KeyValuePair<string, int>(minigame, minigameScore));
                    }
                }

                order.currentStage = order.minigameList.Count;
            }
        }
    }
}