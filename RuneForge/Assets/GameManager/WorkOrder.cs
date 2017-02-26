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
    public bool isEnhanced = false;
    public bool isRandom = false;
    public bool isComplete = false;
    public float multiplier = 1f;
    public List<KeyValuePair<string, int>> minigameList = new List<KeyValuePair<string, int>>();

    public WorkOrder(Item item, int orderNumber, bool isEnhanced, bool isRandom)
    {
        this.item = item;
        this.orderNumber = orderNumber;
        this.requiredStages = item.minigamesRequired;
        this.currentStage = 0;
        this.score = 0;
        this.isEnhanced = isEnhanced;
        this.isRandom = isRandom;
        if (isEnhanced)
        {
            multiplier += 0.5f;
        }
        if (isRandom)
        {
            SetRandomMinigames();
            multiplier += 0.2f;
        }
    }

    public void UpdateOrder(string minigame, int score)
    {
        score = Mathf.RoundToInt(score * multiplier);
        this.score += score;

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
            DetermineQuality();
        }
    }

    public bool CanPlayMinigame(string minigame)
    {
        if (!this.isRandom && !MinigameListContains(minigame))
        {
            return true;
        }
        else if (this.isRandom && MinigameAt(minigame, this.currentStage))
        {
            return true;
        }
        else
            return false;
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
            {
                return true;
            }
        }

        return false;
    }

    void SetRandomMinigames()
    {
        List<string> tempMinigameList = new List<string>();
        for (int i = 0; i < MasterGameManager.instance.minigameList.Count; i++)
        {
            MasterGameManager.Minigame minigame = MasterGameManager.instance.minigameList[i];
            if (!minigame.isTutorial)
                tempMinigameList.Add(MasterGameManager.instance.minigameList[i].Name);
        }

        for (int i = 0; i < requiredStages; i++)
        {
            int randomIndex = Random.Range(0, tempMinigameList.Count);
            minigameList.Add(new KeyValuePair < string, int >(tempMinigameList[randomIndex], int.MinValue));
            tempMinigameList.RemoveAt(randomIndex);
        }
    }

    void DetermineQuality()
    {
        int standard = 0, highQuality = 0, masterCraft = 0;
        foreach (var kvp in minigameList)
        {
            MasterGameManager.Minigame minigame = MasterGameManager.instance.minigameDict[kvp.Key];
            standard += minigame.SD;
            highQuality += minigame.HQ;
            masterCraft += minigame.MC;
        }

        bool successfulRoll;
        if (this.score <= standard) //Roll for Standard
        {
            successfulRoll = WeightedCoinFlip(this.score, standard);
            if (successfulRoll)
                Debug.Log("Rolled for Standard - Got Standard");
            else
                Debug.Log("Rolled for Standard - Failed");
        }
        else if (standard < score && score <= highQuality)  //Roll for HQ
        {
            successfulRoll = WeightedCoinFlip(score - standard, highQuality - standard);
            if (successfulRoll)
            {
                Debug.Log("Rolled for HQ - Got HQ");
                this.item = ItemCollection.itemDict[item.name + " (HQ)"];
            }
            else
                Debug.Log("Rolled for HQ - Got Standard");
        }
        else if (score > highQuality)   //Roll for MC
        {
            successfulRoll = WeightedCoinFlip(score - highQuality, masterCraft - highQuality);
            if (successfulRoll)
            {
                Debug.Log("Rolled for MC - Got MC");
                this.item = ItemCollection.itemDict[item.name + " (MC)"];
            }
            else
                Debug.Log("Rolled for MC - Got HQ");
        }
    }

    bool WeightedCoinFlip(int success, int upperBound)
    {
        int randomInt = Random.Range(0, upperBound + 1);
        return (randomInt <= success);
    }
}