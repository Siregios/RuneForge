using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontPageUIManager : MonoBehaviour {
    public CalendarPage calendarPage;

    void OnEnable()
    {
        calendarPage.DisplayPage();
    }

    public void EndDayClick()
    {
        MasterGameManager.instance.actionClock.EndDay();
    }

    public void SaveGameClick()
    {
        MasterGameManager.instance.saveManager.SaveData();
    }

    public void LoadGameClick()
    {
        MasterGameManager.instance.saveManager.LoadData();
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
