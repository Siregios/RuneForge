using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatmappingScript : MonoBehaviour {

    //write with these
    private string writer;
    private bool written = false;
    public List<float> timeList;
    public string songName;

    void Start()
    {
        //writing stuff
        timeList = new List<float>();
    }

    void Update()
    {
        //ALL WRITING STUFF
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Time.time);
            timeList.Add(Time.time);
        }

        if (!GameObject.Find("AudioSource").GetComponent<AudioSource>().isPlaying)
        {
            foreach (float beat in timeList)
                writer += beat.ToString() + " ";
            if (!written)
            {
                System.IO.File.WriteAllText("C:/Users/DavidTruong/Desktop/RuneForge/RuneForge/Assets/Resources/Beatmaps/" + songName + ".txt", writer + "\n");
                written = true;
            }
        }
        //END WRITING STUFF

    }
}
