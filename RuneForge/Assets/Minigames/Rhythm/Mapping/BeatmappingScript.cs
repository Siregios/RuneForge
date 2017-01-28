using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatmappingScript : MonoBehaviour {

    //write with these
    private string writer;
    private bool written = false;
    public List<double> timeList;
    public string songName;
    float currentBeat;
    bool beatDrop = false;


    void Start()
    {
        //writing stuff
        timeList = new List<double>();
        timeList.Add(AudioSettings.dspTime);
    }

    void Update()
    {
        //ALL WRITING STUFF


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(AudioSettings.dspTime);            
            timeList.Add(AudioSettings.dspTime);
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
                //System.IO.File.WriteAllText("C:/Users/DavidTruong/Desktop/RuneForge/RuneForge/Assets/Resources/Beatmaps/" + songName + ".txt", writer + "\n");
                System.IO.File.WriteAllText("C:/Users/Peter Truong/Documents/GitHub/RuneForge/RuneForge/Assets/Resources/Beatmaps/" + songName + ".txt", writer + "\n");
                written = true;
            }
        }
        //END WRITING STUFF

    }
}
