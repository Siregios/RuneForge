using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private int level = 1;
    private float totalExperience = 0.0f;

    public int Level
    {
        get { return level; }
    }

    public float TotalExperience
    {
        get { return totalExperience; }
    }

    /// <summary>
    /// Adds "increment" to the current player level
    /// </summary>
    /// <param name="increment"></param>
    public void incrementLevel(int increment)
    {
        //We might not need to make this public if we just increment level internally in this script based on experience
        level += increment;
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
    public void gainExperience(float experienceGained)
    {
        totalExperience += experienceGained;
        Debug.Log("I gained " + experienceGained + "EXP");
    }
}