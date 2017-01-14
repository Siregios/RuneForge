using UnityEngine;
using System.Collections;

public class ActionClock : MonoBehaviour {
    private int maxActionsPerDay = 1;
    private int currentActionCount = 1;
    private int day = 1;

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
        MasterGameManager.instance.sceneManager.LoadScene("Store");
        yield return new WaitForSeconds(.01f);  //This is small because loadscene sets timescale to 0
        day++;
        currentActionCount = maxActionsPerDay;
    }
}
