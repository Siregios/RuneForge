using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class RandomSpawnNote : MonoBehaviour
{
    public GameObject[] spawnObject;
    public List<List<GameObject>> keyNotes;
    public int randomInt;
    int dubRandom = 6;
    bool row = false;
    bool ddr = false;
    int loop = 0;
    //private BeatObserver beatObserver;
    //private int beatCounter;

    public double musicSync;
    private double offset;
    //Read with these
    [HideInInspector]
    public TextAsset beatTime;
    private int counter = 0;
    private List<double> readTime;
    public string songName;
    public bool songStart = false;
    [HideInInspector]
    public int dub = 0;
    [HideInInspector]
    public int mult = 1;

    //UI Text
    public Score score;
    public Text multiplierText;
    public Text multText;
    [HideInInspector]
    public int great, perfect, miss, multiplier = 0;

    void Start()
    {
        keyNotes = new List<List<GameObject>>();
        for (int i = 0; i < 4; i++)
            keyNotes.Add(new List<GameObject>());
        songName = "monkeyClap";

        //reading stuff
        readTime = new List<double>();
        beatTime = Resources.Load("Beatmaps/" + songName, typeof(TextAsset)) as TextAsset;

        foreach (string f in beatTime.text.Split())
        {
            double num;
            if (double.TryParse(f, out num))
                readTime.Add(num);
        }
        offset = AudioSettings.dspTime - readTime[0];
        counter++;
        GetComponent<AudioSource>().enabled = true;
        GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime + musicSync);
    }

    void Update()
    {
        if (multiplier > 15)
            mult = 4;
        else if (multiplier > 10)
            mult = 3;
        else if (multiplier > 5)
            mult = 2;
        else
            mult = 1;

        multText.text = "Multiplier: x" + mult.ToString();
        multiplierText.text = "x" + multiplier.ToString();
        if (counter >= readTime.Count && !GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }

        if (counter < readTime.Count)
        {
            if (Mathf.Abs((float)AudioSettings.dspTime - (float)offset) >= readTime[counter])
            {
                if (row)
                    spawnSpecific(randomInt);
                else if (counter + 2 < readTime.Count)
                {
                    if (readTime[counter + 2] - readTime[counter] < 0.5f)
                    {
                        if (Random.Range(1, 3) == 1)
                            spawnRandomNote(true, true, 2);
                        else
                            spawnRandomNote(true, false, 2);
                        //if (Random.Range(1, 3) == 1 && dub < 1)
                        //{
                        //    spawnRandomDouble();
                        //    dub++;
                        //}
                        //else
                        //    spawnRandomNote();
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

    void spawnRandomNote(bool spec = false, bool d = false, int l = 0)
    {
        row = spec;
        loop = l;
        ddr = d;

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

    void spawnSpecific(int dir)
    {
        if (ddr)
        {
            if (loop == 2)
                randomInt++;
            else if (loop == 1)
                randomInt--;
            if (randomInt == 4)
                randomInt = 0;
            if (randomInt == -1)
                randomInt = 3;
        }
        GameObject note = (GameObject)Instantiate(spawnObject[randomInt], spawnObject[randomInt].transform.position, Quaternion.identity);
        note.GetComponent<NoteTracker>().indexNote = randomInt;
        keyNotes[randomInt].Add(note);
        if (loop == 0)
            row = false;
        loop--;
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
