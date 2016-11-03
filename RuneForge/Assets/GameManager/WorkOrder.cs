﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The MinigameController script that Peter is writing should have some reference to a list of work orders that
// will be gaining progress from playing that game.
// It will then subsequently call UpdateOrder during the results screen for each work order in that list.

// [Optional] Perhaps that script should also check if an order has been completed and show the proper result screen for that?
public class WorkOrder {
    public Item item;
    public int orderNumber;
    public int requiredStages;
    public int currentStage;
    public int score;
    public bool isRandom = false;
    public List<KeyValuePair<string, int>> minigameList = new List<KeyValuePair<string, int>>();

    public WorkOrder(Item item, int orderNumber, bool isRandom)
    {
        this.item = item;
        this.orderNumber = orderNumber;
        this.requiredStages = item.minigamesRequired;
        this.currentStage = 0;
        this.score = 0;
        this.isRandom = isRandom;
        if (isRandom)
            SetRandomMinigames();
    }

    public void UpdateOrder(string minigame, int score)
    {
        this.score += score;

        if (!isRandom)
            this.minigameList.Add(new KeyValuePair<string, int>(minigame, score));
        else
        {
            Debug.LogWarningFormat("Not adding minigame - {0} - to Work Order #{1}) {2} because it is randomized", minigame, orderNumber, item.name);
            this.minigameList[currentStage] = new KeyValuePair<string, int>(minigame, score);
        }

        this.currentStage++;

        if (currentStage == requiredStages)
        {
            Debug.LogFormat("Work Order #{0}) {1} has been completed! Score: {2}", orderNumber, item.name, this.score);
            MasterGameManager.instance.workboard.CompleteOrder(this);
        }
    }

    public bool MinigameAt(string minigame, int index)
    {
        return minigameList[index].Key == minigame;
    }

    public bool MinigameListContains(string minigame)
    {
        for (int i = 0; i < minigameList.Count; i++)
        {
            if (MinigameAt(minigame, i))
                return true;
        }

        return false;
    }

    void SetRandomMinigames()
    {
        List<string> tempMinigameList = new List<string>();
        foreach (string minigame in MasterGameManager.instance.minigameList)
            tempMinigameList.Add(minigame);

        for (int i = 0; i < requiredStages; i++)
        {
            int randomIndex = Random.Range(0, tempMinigameList.Count);
            minigameList.Add(new KeyValuePair < string, int >(tempMinigameList[randomIndex], int.MinValue));
            tempMinigameList.RemoveAt(randomIndex);
        }
    }
}