using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDayStats : MonoBehaviour {

    public int day = 0;
    public string season = "";
    public int actions = 0;
    public int money = 0;
    public int level = 0;
    public float experience = 0;
    public int quests = 0;
    public Inventory inventory;
    ActionClock actionClock;
    PlayerStats playerStats;

    void Start()
    {
        inventory = new Inventory(PlayerInventory.inventory);
        copyGameStats();
    }

    public void copyGameStats()
    {
        //master game manager stuff
        ActionClock actionClock = MasterGameManager.instance.actionClock;
        PlayerStats playerStats = MasterGameManager.instance.playerStats;
        day = actionClock.Day;
        season = actionClock.Season;
        actions = actionClock.ActionCount;
        money = PlayerInventory.money;
        level = playerStats.level;
        experience = playerStats.currentExperience;
        quests = 0;
        inventory = new Inventory(PlayerInventory.inventory);
    }
}