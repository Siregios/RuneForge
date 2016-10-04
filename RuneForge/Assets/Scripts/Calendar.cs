using UnityEngine;
using System.Collections;

public class Calendar : MonoBehaviour {
    public int day = 1;
    public int season = 1;
    public float time;
    public float startTime;
    public float dayLength;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        time = Time.time;
        if(time - startTime > dayLength)
        {
            startTime = Time.time;
            day++;
        }
	    if(day > 30)
        {
            day = 1;
            if(season > 4)
                season = 1;
            else
                season++;
        }
	}
}
