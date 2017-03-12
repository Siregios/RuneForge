using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarPage : MonoBehaviour {
    public Image calendar;
    public Sprite springCalendar, summerCalendar, fallCalendar, winterCalendar;

    public void DisplayCalendar()
    {
        string season = MasterGameManager.instance.actionClock.Season;
        if (season == "spring")
            calendar.sprite = springCalendar;
        else if (season == "summer")
            calendar.sprite = summerCalendar;
        else if (season == "fall")
            calendar.sprite = fallCalendar;
        else if (season == "winter")
            calendar.sprite = winterCalendar;
    }
}
