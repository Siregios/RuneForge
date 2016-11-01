﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;
using System.IO;
using System.Text;

public class RandomSpawnNote : MonoBehaviour
{
    public GameObject[] spawnObject;
    int randomInt = 1;
    //private BeatObserver beatObserver;
    //private int beatCounter;


    //Read with these
    public TextAsset beatTime;
    private int counter = 0;
    private List<float> readTime;
    public string songName;
    private float startTime;
    public bool songStart = false;
    //write with these
    //private string writer;
    //private bool written = false;
    //public List<float> timeList;

    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width, false);
        startTime = Time.time;
        //beatObserver = GetComponent<BeatObserver>();
        //beatCounter = 0;
        songName = "monkeyClap";

        //reading stuff
        readTime = new List<float>();
        beatTime = Resources.Load("Beatmaps/" + songName, typeof(TextAsset)) as TextAsset;

        foreach (string f in beatTime.text.Split())
        {
            float num;
            if (float.TryParse(f, out num))
                readTime.Add(num);
        }

        //writing stuff
        //timeList = new List<float>();

    }

    void Update()
    {
        if (counter >= readTime.Count && !GameObject.Find("AudioSource").GetComponent<AudioSource>().isPlaying)
        {
            MasterGameManager.instance.sceneManager.LoadScene("Workshop");
        }
        //ALL WRITING STUFF 
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log(Time.time);
        //    timeList.Add(Time.time);
        //}

        //if (!GameObject.Find("AudioSource").GetComponent<AudioSource>().isPlaying)
        //{
        //    foreach (float beat in timeList)
        //        writer += beat.ToString() + " ";
        //    if (!written)
        //    {
        //        System.IO.File.WriteAllText("C:/Users/petergtruong/Desktop/RuneForge/RuneForge/Assets/Resources/Beatmaps/" + songName + ".txt", writer + "\n");
        //        written = true;
        //    }
        //}
        //END WRITING STUFF

        if (counter < readTime.Count)
        {
            if (counter == 1)
                songStart = true;
            if (Time.time > readTime[counter] + startTime)
            {
                randomInt = Random.Range(1, 5);
                if (randomInt == 1)
                    Instantiate(spawnObject[0], spawnObject[0].transform.position, Quaternion.identity);
                else if (randomInt == 2)
                    Instantiate(spawnObject[1], spawnObject[1].transform.position, Quaternion.identity);
                else if (randomInt == 3)
                    Instantiate(spawnObject[2], spawnObject[2].transform.position, Quaternion.identity);
                else if (randomInt == 4)
                    Instantiate(spawnObject[3], spawnObject[3].transform.position, Quaternion.identity);
                counter++;
            }
        }
    }
}
