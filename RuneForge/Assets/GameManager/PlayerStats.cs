﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int level = 1;
    public int currentExperience = 0;

    private int maxLevel = 5;
    private List<int> levelUp = new List<int> { 0, 10000, 30000, 60000, 100000 };   //zero is just so no errors happen

    void Start()
    {
        setShopItems();
    }

    public int nextLevelUp()
    {
        //for (int i = 0; i < levelUp.Length; i++)
        //    if (currentExperience < levelUp[i])
        //        return levelUp[i];
        //return levelUp[levelUp.Length - 1];
        return levelUp[level];
    }

    public int previousLevelUp()
    {
        //for (int i = 0; i < levelUp.Length; i++)
        //    if (nextLevelUp() == levelUp[i])
        //        if (i != 0)
        //            return levelUp[i];
        //return 0;
        return levelUp[level - 1];
    }

    /// <summary>
    /// Adds "increment" to the current player level
    /// </summary>
    /// <param name="increment"></param>
    public void incrementLevel()
    {
        //We might not need to make this public if we just increment level internally in this script based on experience
        while(level < maxLevel && levelUp[level] <= currentExperience)
        {
            //currentExperience -= levelUp[level - 1];
            level++;
        }

        setShopItems();
    }

    void setShopItems()
    {
        foreach (Item material in ItemCollection.FilterItemList("material"))
        {
            if (material.level <= level)
                ShopInventory.inventory.SetItemCount(material, int.MaxValue);
        }
    }

    /// <summary>
    /// Adds "experienceGained" to player's totalExperience
    /// </summary>
    /// <param name="experienceGained"></param>
    public void gainExperience(int experienceGained)
    {
        currentExperience += experienceGained;
        //incrementLevel(); <-- This is done in result screen now
    }
}