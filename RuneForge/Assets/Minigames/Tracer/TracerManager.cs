﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {
    public GameObject lineRenderer = null;
    public GameObject trailRenderer = null;
    public GameObject burst;
    public GameObject nextMapBurst;
    Vector3 currentPos;

    private AudioManager AudioManager;
    private AudioSource music;
    //Number of maps that exist in Assets/TraceMaps
    public int traceMapCount = 4;
    int currentMap = 4;
    //Number of maps to spawn in one playthrough
    public int mapsPerPlay = 5;
    public float spawnInterval = 1f;
    int count = 1;

    public Score score;

    GameObject currentTrail = null;

    void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        music = this.gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        CreateNewTrail();
        Cursor.visible = false;
        music.Play();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTrail.transform.position = mousePos;        
    }

    public void DotTouched(GameObject dot)
    {
        AudioManager.PlaySound(0);
       
        score.addScore(10);
        
        currentTrail.GetComponent<TrailRenderer>().time += .01f;

        currentPos = dot.transform.position;

        //This makes the last dot not spawn burst prefab
        if (dot.GetComponent<Mapper>().next != null || count < mapsPerPlay)
            Destroy((GameObject)Instantiate(burst, currentPos, Quaternion.identity), 0.2f);

        SpawnDot(dot);
    }

    public void DotMissed(GameObject dot)
    {
        SpawnDot(dot);
    }

    public void SpawnDot(GameObject dot)
    {
        if (dot.GetComponent<Mapper>().next != null)
        {            
            dot.GetComponent<Mapper>().next.SetActive(true);
        }
        else {
            if (count < mapsPerPlay)
            {
                int randomMapNumber;
                do
                {
                    randomMapNumber = Random.Range(1, traceMapCount + 1);
                    Debug.Log(randomMapNumber);
                } while (randomMapNumber == currentMap);
                currentMap = randomMapNumber;
                Debug.LogFormat("Spawning Map{0}", randomMapNumber);
                //Instantiate prefab transition and give it time to run
                Destroy((GameObject)Instantiate(nextMapBurst, new Vector3(0, 0, 0), Quaternion.identity), 0.8f);
                StartCoroutine(Wait(randomMapNumber));
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
                currentTrail.SetActive(false);
                Cursor.visible = true;
                //Should show results screen here first.
                GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
            }
        }
    }

    //This works after 0.65 seconds to let the particle animation run
    IEnumerator Wait(int randomMapNumber)
    {
        yield return new WaitForSeconds(0.65f);
        Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
        Instantiate(Resources.Load("TraceMaps/Map" + randomMapNumber));
        AudioManager.PlaySound(1);
        count++;
    }
    void CreateNewTrail()
    {
        currentTrail = (GameObject)Instantiate(trailRenderer);
    }
}
