using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;

public class BeatmappingScript : MonoBehaviour {

    //write with these
    private string writer;
    private bool written = false;
    public List<float> timeList;
    public string songName;
    float currentBeat;

    private BeatObserver beatObserver;
    private int beatCounter;

    void Start()
    {
        //writing stuff
        timeList = new List<float>();
        beatObserver = GetComponent<BeatObserver>();
        beatCounter = 0;
    }

    void Update()
    {
        //ALL WRITING STUFF
        if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
            currentBeat = Time.time;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - currentBeat < 0)
                timeList.Add(currentBeat);
            else
                timeList.Add(Time.time);
        }

        if (!GameObject.Find("AudioSource").GetComponent<AudioSource>().isPlaying)
        {
            foreach (float map in timeList)
            {                
                writer += map.ToString() + " ";                
            }

            if (!written)
            {
                System.IO.File.WriteAllText("C:/Users/DavidTruong/Desktop/RuneForge/RuneForge/Assets/Resources/Beatmaps/" + songName + ".txt", writer + "\n");
                written = true;
            }
        }
        //END WRITING STUFF

    }
}
