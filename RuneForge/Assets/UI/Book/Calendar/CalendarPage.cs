using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarPage : MonoBehaviour {
    public Text actionsText;
    public Image currentDateCircle;
    public Image calendar;
    public Sprite springCalendar, summerCalendar, fallCalendar, winterCalendar;

    public void DisplayPage()
    {
        DisplayCalendar();
        actionsText.text = MasterGameManager.instance.actionClock.ActionCount.ToString();
        CircleCurrentDate();
    }

    void DisplayCalendar()
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

    void CircleCurrentDate()
    {
        int xPad = 25, yPad = -23;

        int day = MasterGameManager.instance.actionClock.Day;

        float yPos = yPad * (Mathf.FloorToInt((day-1) / 7));
        float xPos = xPad * ((day-1) % 7);

        currentDateCircle.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
    }
}
