using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float time = 30;
    [HideInInspector]
    public bool timeEnd = false;
    public bool displayTimerText;
    [HideInInspector]
    public bool stopTimer;
    public Text timerText;    

    void Awake () {
        if (displayTimerText)
            timerText.text = Mathf.CeilToInt(time).ToString();
    }
	
	
	void Update () {
        if (time <= 0)
            timeEnd = true;
        else if (!stopTimer)
            time -= Time.deltaTime;

        if (displayTimerText)
        {
            timerText.text = Mathf.CeilToInt(time).ToString();
        }
    }
}
