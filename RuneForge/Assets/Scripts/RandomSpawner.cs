﻿using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour {
    public bool thereCanOnlyBeOne = false;
    public float spawnInterval = 1f;
    public GameObject objectToSpawn;
    GameObject previousObject = null;
    Vector3 worldBounds;
    float cooldown;
    int pad = 1;

    void Start()
    {
        cooldown = spawnInterval;
        worldBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        if (thereCanOnlyBeOne)
            Destroy(previousObject);
        Vector3 randomPos;
        randomPos = new Vector3(Random.Range(-worldBounds.x + pad, worldBounds.x - pad),
                                Random.Range(-worldBounds.y + pad, worldBounds.y - pad), 0);
        previousObject = Instantiate(objectToSpawn, randomPos, Quaternion.identity) as GameObject;
        cooldown = spawnInterval;
    }
}
