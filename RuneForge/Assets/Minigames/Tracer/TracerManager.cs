﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {
    public GameObject lineRenderer = null;
    public GameObject trailRenderer = null;
    public GameObject burst;
    public GameObject nextMapBurst;
    Vector3 currentPos;

    private AudioSource music;
    public List<TraceMap> traceMaps;
    int currentMapIndex = -1;
    TraceMap currentMap;
    //Number of maps to spawn in one playthrough
    public int mapsPerPlay = 5;
    public float spawnInterval = 1f;
    public int count = 1;

    public Score score;

    public AudioClip hitSound;
    public AudioClip completionSound;

    GameObject currentTrail = null;

    void Start()
    {
        currentMap = CreateNewMap();
        CreateNewTrail();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTrail.transform.position = new Vector3(mousePos.x, mousePos.y, -5f);
    }

    public void DotTouched(GameObject dot)
    {
        int points = (int)(3 * Mathf.Ceil(4 + Mathf.Min(7 * Mathf.Pow(dot.GetComponent<DotController>().timeRemaining/dot.GetComponent<DotController>().lifeTime,4), 6)));
        score.addScore(points);

        currentPos = dot.transform.position;

        //This makes the last dot not spawn burst prefab
        if (!currentMap.onLastDot || count < mapsPerPlay)
            Destroy((GameObject)Instantiate(burst, currentPos, Quaternion.identity), 0.2f);
        currentMap.dotsHit++;
        SpawnDot(dot);

        //Aesthetics polish
        MasterGameManager.instance.audioManager.PlaySFXClip(hitSound);
        currentTrail.GetComponent<TrailRenderer>().time += .01f;
        currentMap.AdvanceSprites();
    }

    public void DotMissed(GameObject dot)
    {
        currentTrail.GetComponent<TrailRenderer>().time = .01f;
        SpawnDot(dot);
    }

    public void SpawnDot(GameObject dot)
    {
        if (!currentMap.onLastDot)
        {
            currentMap.ActivateNextDot();
        }
        else {
            //If there's still more maps to go, continue by spawning next map
            if (count < mapsPerPlay)
            {
                //Instantiate prefab transition and give it time to run
                Destroy((GameObject)Instantiate(nextMapBurst, new Vector3(0, 0, 0), Quaternion.identity), 0.8f);
                StartCoroutine(NextMap());
            }
            //If played enough maps this playthrough, end the minigame and show results
            else
            {
                Destroy(currentMap.gameObject);
                currentTrail.SetActive(false);
                Cursor.visible = true;
                GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
            }
        }
    }

    TraceMap CreateNewMap()
    {
        int randomMapNumber;
        do
        {
            randomMapNumber = Random.Range(0, traceMaps.Count);
            Debug.Log(randomMapNumber);
        } while (randomMapNumber == currentMapIndex);
        currentMapIndex = randomMapNumber;
        Debug.LogFormat("Spawning Map{0}", randomMapNumber);

        GameObject newTraceMap = (GameObject)Instantiate(traceMaps[randomMapNumber].gameObject);
        return newTraceMap.GetComponent<TraceMap>();
    }

    //This works after 0.65 seconds to let the particle animation run
    IEnumerator NextMap()
    {
        yield return new WaitForSeconds(0.65f);

        Destroy(currentMap.gameObject);
        currentMap = CreateNewMap();
        MasterGameManager.instance.audioManager.PlaySFXClip(completionSound);
        count++;
    }

    void CreateNewTrail()
    {
        currentTrail = (GameObject)Instantiate(trailRenderer);
        currentTrail.GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
    }
}
