﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HookManager : MonoBehaviour {

    RandomSpawner spawner;
    public float spawnInterval = 2f;
    public int maxObjects = 10;
    public int currentObjects = 0;
    float cooldown;

    public Text textRemaining;
    public Text scoreText;
    public int remainingHooks = 5;

    [HideInInspector]
    public int score = 0;



    void Awake ()
    {
        spawner = this.GetComponent<RandomSpawner>();
    }

    void Start ()
    {
        cooldown = spawnInterval;
    }

    void Update()
    {
        textRemaining.text = "Hooks Remaining: " + remainingHooks.ToString();
        cooldown -= Time.deltaTime;
        scoreText.text = "Score: " + score.ToString();

        if (cooldown <= 0 && currentObjects < maxObjects)
        {
            SpawnObject();
            currentObjects++;
        }
    }

    public void SpawnObject()
    {
        cooldown = spawnInterval;
        spawner.SpawnObject();
    }
}

