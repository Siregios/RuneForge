using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionClock : MonoBehaviour {
    private int maxActionsPerDay = 1;
    private int currentActionCount = 1;
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
    }

    public void EndDay()
    {
        StartCoroutine(endCoroutine());
    }

    public void SpendAction()
    {
        if (currentActionCount > 0)
            currentActionCount--;
        else
            Debug.LogError("Cannot perform action, not enough time today");
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
        currentActionCount = maxActionsPerDay;
        rent();
        MasterGameManager.instance.questGenerator.GenerateQuests();

        MasterGameManager.instance.sceneManager.LoadScene("Store");
        yield return new WaitForSeconds(.01f);  //This is small because loadscene sets timescale to 0
    }

    void rent()
    {
        switch (gameObject.GetComponent<PlayerStats>().Level)
        {
            case 1:
                PlayerInventory.money -= 100;
                break;
            case 2:
                PlayerInventory.money -= 200;
                break;
            case 3:
                PlayerInventory.money -= 300;
                break;
            case 4:
                PlayerInventory.money -= 400;
                break;
            case 5:
                PlayerInventory.money -= 500;
                break;
            default:
                break;
        }
        if(PlayerInventory.money < 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}
