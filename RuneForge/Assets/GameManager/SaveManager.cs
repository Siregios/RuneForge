using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public void SaveData()
    {
        PlayerPrefs.SetString("@General: Scene", MasterGameManager.instance.sceneManager.currentScene);
        SavePlayerStats();
        SavePlayerInventory();
        SaveActionClock();

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        LoadPlayerStats();
        LoadPlayerInventory();
        LoadActionClock();
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
}
