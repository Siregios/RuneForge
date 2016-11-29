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
    public bool isComplete = false;
    public float multiplier = 1f;
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
        {
            SetRandomMinigames();
            multiplier += 0.5f;
        }
    }

    public void UpdateOrder(string minigame, int score)
    {
        this.score += Mathf.RoundToInt(score * multiplier);

        if (!isRandom)
            this.minigameList.Add(new KeyValuePair<string, int>(minigame, score));
        else
        {
            this.minigameList[currentStage] = new KeyValuePair<string, int>(minigame, score);
        }

        this.currentStage++;

        if (currentStage == requiredStages)
        {
            Debug.LogFormat("Work Order #{0}) {1} has been completed! Score: {2}", orderNumber, item.name, this.score);
            isComplete = true;
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
        for(int i = 0; i < MasterGameManager.instance.minigameList.Count; i++)//string minigame in MasterGameManager.instance.minigameList)
            tempMinigameList.Add(MasterGameManager.instance.minigameList[i].Name);

        for (int i = 0; i < requiredStages; i++)
        {
            int randomIndex = Random.Range(0, tempMinigameList.Count);
            minigameList.Add(new KeyValuePair < string, int >(tempMinigameList[randomIndex], int.MinValue));
            tempMinigameList.RemoveAt(randomIndex);
        }
    }
}