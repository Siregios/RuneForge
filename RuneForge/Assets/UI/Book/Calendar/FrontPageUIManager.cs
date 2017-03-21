using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontPageUIManager : MonoBehaviour {
    public CalendarPage calendarPage;
    public AudioClip endDaySound;
    public AudioClip saveGameSound;
    public AudioClip loadGameSound;
    public AudioClip quitGameSound;
    void OnEnable()
    {
        calendarPage.DisplayPage();
    }

    public void EndDayClick()
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(endDaySound);
        MasterGameManager.instance.actionClock.EndDay();
    }

    public void SaveGameClick()
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(saveGameSound);
        MasterGameManager.instance.saveManager.SaveData();
    }

    public void LoadGameClick()
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(loadGameSound);
        MasterGameManager.instance.saveManager.LoadData();
    }

    public void QuitClick()
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(quitGameSound);
        MasterGameManager.instance.saveManager.SaveData();
        Application.Quit();
    }
}
