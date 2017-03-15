using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionClock : MonoBehaviour {
    private int maxActionsPerDay = 6;
    private int currentActionCount = 6;
    private int day = 1;
    private int seasonIndex = 1;
    List<string> seasonMap = new List<string> { "spring", "summer", "fall", "winter" };

    public int ActionsPerDay
    {
        get { return maxActionsPerDay; }
        set { maxActionsPerDay = value; }
    }

    public int ActionCount
    {
        get { return currentActionCount; }
        set { currentActionCount = value; }
    }

    public int Day
    {
        get { return day; }
        set { day = value; }
    }

    public string Season
    {
        get { return seasonMap[seasonIndex - 1]; }
        set
        {
            string season = value;
            if (season == "spring")
                seasonIndex = 1;
            else if (season == "summer")
                seasonIndex = 2;
            else if (season == "fall")
                seasonIndex = 3;
            else if (season == "winter")
                seasonIndex = 4;
        }
    }

    public void EndDay()
    {
        if (!MasterGameManager.instance.sceneManager.loadingScene)
            StartCoroutine(endCoroutine());
    }

    public bool SpendAction()
    {
        if (currentActionCount > 0)
        {
            currentActionCount--;
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator endCoroutine()
    {
        day++;
        if (day > 28)
        {
            day = 1;
            seasonIndex++;
            if (seasonIndex > 4)
                seasonIndex = 1;
        }

        if (MasterGameManager.instance.upgradeManager.level2 == 2 || MasterGameManager.instance.upgradeManager.level2 == 3)
            maxActionsPerDay = 8;
        else
            maxActionsPerDay = 6;

        rent();
        MasterGameManager.instance.questGenerator.GenerateQuests();

        MasterGameManager.instance.sceneManager.LoadScene("EndDay");
        yield return new WaitForSeconds(.01f);  //This is small because loadscene sets timescale to 0
    }

    void rent()
    {
        int reduction;
        if (MasterGameManager.instance.upgradeManager.level4 == 2 || MasterGameManager.instance.upgradeManager.level4 == 3)
            reduction = 4;
        else
            reduction = 1;

            switch (gameObject.GetComponent<PlayerStats>().level)
            {
                case 1:
                    PlayerInventory.money -= 100/reduction;
                    break;
                case 2:
                    PlayerInventory.money -= 200/reduction;
                    break;
                case 3:
                    PlayerInventory.money -= 300/reduction;
                    break;
                case 4:
                    PlayerInventory.money -= 400/reduction;
                    break;
                case 5:
                    PlayerInventory.money -= 500/reduction;
                    break;
                default:
                    break;
            }
        if(PlayerInventory.money < 0)
        {
            PlayerInventory.money = 0; //Temporary for presentation
            Debug.Log("GAME OVER");
        }
    }
}
