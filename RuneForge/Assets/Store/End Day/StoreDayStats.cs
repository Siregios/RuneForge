using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDayStats : MonoBehaviour {

    int day = 0;
    string season = "";
    int actions = 0;
    int money = 0;
    int level = 0;
    float experience = 0;

    //master game manager stuff
    ActionClock actionClock = MasterGameManager.instance.actionClock;
    PlayerStats playerStats = MasterGameManager.instance.playerStats;    
    Inventory inventory = new Inventory();

    public void copyGameStats()
    {
        day = actionClock.Day;
        season = actionClock.Season;
        actions = actionClock.ActionCount;
        money = PlayerInventory.money;
        level = playerStats.Level;
        experience = playerStats.CurrentExperience;
    }
}
