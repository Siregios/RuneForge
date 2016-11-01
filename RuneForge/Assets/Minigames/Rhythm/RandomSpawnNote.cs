using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class RandomSpawnNote : MonoBehaviour
{
    public GameObject[] spawnObject;
    public List<Object> keyNotes;
    int randomInt;
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

    //UI Text
    public Text scoreText;
    [HideInInspector]
    public int score = 0;
    public Text hitText;
    public Text track;
    [HideInInspector]
    public int good, great, perfect, miss = 0;
    
    //write with these
    //private string writer;
    //private bool written = false;
    //public List<float> timeList;


    void Start()
    {
        keyNotes = new List<Object>();
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
        scoreText.text = score.ToString();
        track.text = "Miss: " + miss.ToString() + "\nGood: " + good.ToString() + "\nGreat: " + great.ToString() + "\nPerfect: " + perfect.ToString();
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
            if (counter >= 1)
                songStart = true;
            if (Time.time > readTime[counter] + startTime)
            {
                if (counter + 1 < readTime.Count)
                {
                    if (readTime[counter + 1] - readTime[counter] < 1f)
                    {
                        if (Random.Range(1, 5) == 1)                        
                            spawnRandomDouble();                        
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
        randomInt = Random.Range(1, 5);
        if (randomInt == 1)
            keyNotes.Add(Instantiate(spawnObject[0], spawnObject[0].transform.position, Quaternion.identity));
        else if (randomInt == 2)
            keyNotes.Add(Instantiate(spawnObject[1], spawnObject[1].transform.position, Quaternion.identity));
        else if (randomInt == 3)
            keyNotes.Add(Instantiate(spawnObject[2], spawnObject[2].transform.position, Quaternion.identity));
        else if (randomInt == 4)
            keyNotes.Add(Instantiate(spawnObject[3], spawnObject[3].transform.position, Quaternion.identity));
        counter++;
    }

    void spawnRandomDouble()
    {
        randomInt = Random.Range(1, 5);
        if (randomInt == 1)
            keyNotes.Add(Instantiate(spawnObject[4], spawnObject[4].transform.position, Quaternion.identity));
        else if (randomInt == 2)
            keyNotes.Add(Instantiate(spawnObject[5], spawnObject[5].transform.position, Quaternion.identity));
        else if (randomInt == 3)
            keyNotes.Add(Instantiate(spawnObject[6], spawnObject[6].transform.position, Quaternion.identity));
        else if (randomInt == 4)
            keyNotes.Add(Instantiate(spawnObject[7], spawnObject[7].transform.position, Quaternion.identity));
        counter += 2;
    }
}
