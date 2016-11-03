﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {
    public GameObject lineRenderer = null;
    public GameObject trailRenderer = null;

    public AudioClip dotHit;
    public AudioClip mapLoad;
    AudioSource audio;

    //Number of maps that exist in Assets/TraceMaps
    public int traceMapCount = 4;
    //Number of maps to spawn in one playthrough
    public int mapsPerPlay = 5;
    public float spawnInterval = 1f;
    int count = 1;

    [HideInInspector]
    public int score = 0;

    GameObject currentTrail = null;

    void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
        CreateNewTrail();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTrail.transform.position = mousePos;        
    }

    public void DotTouched(GameObject dot)
    {
        audio.PlayOneShot(dotHit, 1);

        score += 10;
        
        currentTrail.GetComponent<TrailRenderer>().time += .01f;
        
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
                int randomMapNumber = Random.Range(1, traceMapCount + 1);
                Debug.LogFormat("Spawning Map{0}", randomMapNumber);
                Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
                audio.PlayOneShot(mapLoad, 1);
                Instantiate(Resources.Load("TraceMaps/Map" + randomMapNumber));
                count++;
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

    void CreateNewTrail()
    {
        currentTrail = (GameObject)Instantiate(trailRenderer);
    }
}
