using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontPageUIManager : MonoBehaviour {
    public CalendarPage calendarPage;
    public SettingsPage settingsPage;

    void OnEnable()
    {
        calendarPage.DisplayCalendar();
    }
}
