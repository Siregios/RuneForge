﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class RandomSpawnNote : MonoBehaviour
{
    public GameObject[] spawnObject;
    public List<List<GameObject>> keyNotes;
    public int randomInt;
    int dubRandom = 6;
    //private BeatObserver beatObserver;
    //private int beatCounter;


    //Read with these
    [HideInInspector]
    public TextAsset beatTime;
    private int counter = 0;
    private List<float> readTime;
    public string songName;
    private float startTime;
    public bool songStart = false;
    public int dub = 0;

    //UI Text
    public Score score;
    public Text hitText;
    public Text track;
    [HideInInspector]
    public int good, great, perfect, miss = 0;
    
    void Start()
    {
        keyNotes = new List<List<GameObject>>();
        for (int i = 0; i < 4; i++)
            keyNotes.Add(new List<GameObject>());
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
    }

    void Update()
    {
        track.text = "Miss: " + miss.ToString() + "\nGood: " + good.ToString() + "\nGreat: " + great.ToString() + "\nPerfect: " + perfect.ToString();
        if (counter >= readTime.Count && !GameObject.Find("AudioSource").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }

        if (counter < readTime.Count)
        {
            if (counter >= 1)
                songStart = true;
            if (Time.time > readTime[counter] + startTime)
            {
                if (counter + 1 < readTime.Count)
                {
                    if (readTime[counter + 1] - readTime[counter] < 0.5f)
                    {
                        if (Random.Range(1, 3) == 1 && dub < 1)
                        {
                            spawnRandomDouble();
                            dub++;
                        }
                        else
                            spawnRandomNote();
                    }
                    else                   
                        spawnRandomNote();                   
                }
                else
                {
                    counter++;  
                }
            }
        }
    }

    void spawnRandomNote()
    {
        randomInt = Random.Range(0, 4);
        if (randomInt == dubRandom)
        {
            randomInt -= 1;
            if (randomInt == -1)
                randomInt = 3;
            dubRandom = 5;
        }
        GameObject note = (GameObject)Instantiate(spawnObject[randomInt], spawnObject[randomInt].transform.position, Quaternion.identity);
        note.GetComponent<NoteTracker>().indexNote = randomInt;
        keyNotes[randomInt].Add(note);
        
        counter++;
    }

    void spawnRandomDouble()
    {
        randomInt = Random.Range(0, 4);
        dubRandom = randomInt - 1;
        if (dubRandom == -1)
            dubRandom = 3;
        GameObject note = (GameObject)Instantiate(spawnObject[randomInt + 4], spawnObject[randomInt + 4].transform.position, Quaternion.identity);
        note.GetComponent<NoteTracker>().indexNote = randomInt;
        keyNotes[randomInt].Add(note);
        counter += 2;
    }
}
