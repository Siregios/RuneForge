using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameManager : MonoBehaviour
{
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static MasterGameManager instance = null;
    private int maxActionsPerDay = 12;
    private int currentActionCount = 12;
    private int day = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

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