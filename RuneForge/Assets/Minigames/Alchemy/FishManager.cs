﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text timer;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("ProjectileSpawner").GetComponent<ProjectileSpawner>().timeRemaining <= 0)
            MasterGameManager.instance.sceneManager.LoadScene("Workshop");
        scoreText.text = "Score: " + score.ToString();
    }



}
