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
    public List<List<GameObject>> keyNotes;
    public int randomInt;
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
    
    void Start()
    {
        keyNotes = new List<List<GameObject>>();
        for (int i = 0; i < 4; i++)
            keyNotes.Add(new List<GameObject>());
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
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
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
                    if (readTime[counter + 1] - readTime[counter] < 0.2f)
                    {
                        if (Random.Range(1, 3) == 1)                        
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
        randomInt = Random.Range(0, 4);
        GameObject note = (GameObject)Instantiate(spawnObject[randomInt], spawnObject[randomInt].transform.position, Quaternion.identity);
        note.GetComponent<NoteTracker>().indexNote = randomInt;
        keyNotes[randomInt].Add(note);
        
        counter++;
    }

    void spawnRandomDouble()
    {
        randomInt = Random.Range(0, 4);
        GameObject note = (GameObject)Instantiate(spawnObject[randomInt + 4], spawnObject[randomInt + 4].transform.position, Quaternion.identity);
        note.GetComponent<NoteTracker>().indexNote = randomInt;
        keyNotes[randomInt].Add(note);
        counter += 2;
    }
}
