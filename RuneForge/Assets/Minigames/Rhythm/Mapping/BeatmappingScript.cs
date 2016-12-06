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
    bool beatDrop = false;

    private BeatObserver beatObserver;
//    private int beatCounter;

    void Start()
    {
        //writing stuff
        timeList = new List<float>();
        beatObserver = GetComponent<BeatObserver>();
        //beatCounter = 0;
    }

    void Update()
    {
        //ALL WRITING STUFF
        if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
        {
            currentBeat = Time.time;
            beatDrop = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && beatDrop)
        {
            Debug.Log(Time.time);            
            timeList.Add(currentBeat);
            beatDrop = false;
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
                //System.IO.File.WriteAllText("C:/Users/Peter Truong/Documents/GitHub/RuneForge/RuneForge/Assets/Resources/Beatmaps/" + songName + ".txt", writer + "\n");
                written = true;
            }
        }
        //END WRITING STUFF

    }
}
