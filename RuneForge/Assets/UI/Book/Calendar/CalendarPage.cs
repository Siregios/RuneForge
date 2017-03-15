using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarPage : MonoBehaviour {
    public Image expBar;
    public Text levelText;
    public Text actionsText;
    public Image currentDateCircle;
    public GameObject previousDayCross;
    List<GameObject> daysCrossed = new List<GameObject>();
    public Image calendar;
    public Sprite springCalendar, summerCalendar, fallCalendar, winterCalendar;

    public void DisplayPage()
    {
        SetExpBar();
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

    void SetExpBar()
    {
        PlayerStats playerStats = MasterGameManager.instance.playerStats;
        if (levelText != null)
            levelText.text = playerStats.level.ToString();
        if (expBar != null)
        {
            int experience = playerStats.currentExperience - playerStats.previousLevelUp();
            int experienceDelta = playerStats.nextLevelUp() - playerStats.previousLevelUp();
            float expPercentage = (float)experience / (float)experienceDelta;
            expBar.fillAmount = Mathf.Clamp(expPercentage, 0, 1);
        }
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

    public void OnBarHover(bool active)
    {
        if (active)
        {
            HoverInfo.Load();
            HoverInfo.instance.DisplayText(expBar.gameObject, string.Format("Level: {0}\nExperience: {1}\nNext Level: {2}", 
                MasterGameManager.instance.playerStats.level, MasterGameManager.instance.playerStats.currentExperience, MasterGameManager.instance.playerStats.nextLevelUp()));
        }
        else
        {
            HoverInfo.instance.Hide();
        }
    }
}
