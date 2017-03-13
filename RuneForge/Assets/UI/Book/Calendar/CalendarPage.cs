using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarPage : MonoBehaviour {
    public Text actionsText;
    public Image currentDateCircle;
    public GameObject previousDayCross;
    List<GameObject> daysCrossed = new List<GameObject>();
    public Image calendar;
    public Sprite springCalendar, summerCalendar, fallCalendar, winterCalendar;

    public void DisplayPage()
    {
        DisplayCalendar();
        actionsText.text = MasterGameManager.instance.actionClock.ActionCount.ToString();
        crossPreviousDays();
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

    void crossPreviousDays()
    {        
        int xPos = -75, yPos = 25, xPad = 25, yPad = 24;
        for (int day = daysCrossed.Count+1; day < MasterGameManager.instance.actionClock.Day; day++)
        {

            daysCrossed.Add(Instantiate(previousDayCross));            
            daysCrossed[day - 1].GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
            daysCrossed[day - 1].transform.SetParent(transform.FindChild("Calendar"), false);
            xPos += xPad;
            if (day % 7 == 0)
            {
                xPos = -75;
                yPos -= yPad;
            }
        }
    }
}
