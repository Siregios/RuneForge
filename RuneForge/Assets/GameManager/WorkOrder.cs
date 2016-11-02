using UnityEngine;
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
    public List<string> minigameList = new List<string>();

    public WorkOrder(Item item, int orderNumber, bool isRandom)
    {
        this.item = item;
        this.orderNumber = orderNumber;
        this.requiredStages = item.minigamesRequired;
        this.currentStage = 0;
        this.score = 0;
        this.isRandom = isRandom;
    }

    public void UpdateOrder(string minigame, int score)
    {
        this.score += score;

        if (!isRandom)
            this.minigameList.Add(minigame);
        else
            Debug.LogWarningFormat("Not adding minigame - {0} - to Work Order #{1}) {2} because it is randomized", minigame, orderNumber, item.name);

        this.currentStage++;

        if (currentStage == requiredStages)
            Debug.LogFormat("Work Order #{0}) {1} has been completed! Score: {2}", orderNumber, item.name, this.score);
    }
}