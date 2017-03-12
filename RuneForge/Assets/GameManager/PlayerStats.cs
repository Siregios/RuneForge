using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private int level = 1;
    private int maxLevel = 5;
    private int currentExperience = 0;
    private int[] levelUp = new int[] {10000, 20000, 30000, 40000, 0}; //zero is just so no errors happen

    public int Level
    {
        get { return level; }
    }

    public float CurrentExperience
    {
        get { return currentExperience; }
    }

    /// <summary>
    /// Adds "increment" to the current player level
    /// </summary>
    /// <param name="increment"></param>
    public void incrementLevel()
    {
        //We might not need to make this public if we just increment level internally in this script based on experience
        while(level < maxLevel && levelUp[level - 1] <= currentExperience)
        {
            currentExperience -= levelUp[level - 1];
            level++;
        }
        
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
        incrementLevel();
    }
}