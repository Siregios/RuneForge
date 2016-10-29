using UnityEngine;
using System.Collections;

public class ActionClock : MonoBehaviour {
    private int maxActionsPerDay = 12;
    private int currentActionCount = 12;
    private int day = 10;

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

    public void EndDay()
    {
        day++;
        currentActionCount = maxActionsPerDay;
    }

    public void SpendAction()
    {
        if (currentActionCount > 0)
            currentActionCount--;
        else
            Debug.LogError("Cannot perform action, not enough time today");
    }
}
