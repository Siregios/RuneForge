﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigameCountdown : MonoBehaviour {

    int max = 3;
    int count;
    public GameObject[] activate;
    
	void Start () {
        StartGame(false);
        StartCoroutine(Countdown(max));
	}
	

	void Update () {	    
	}

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        GetComponent<AudioSource>().Play();   
        while (count > -1)
        {
            if (count == 0)
                GetComponent<Text>().text = "Go!";
            else
                GetComponent<Text>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        StartGame(true);
        gameObject.SetActive(false);
    }

    void StartGame(bool set)
    {
        foreach (GameObject a in activate)
        {
            foreach (Transform child in a.transform)
                child.gameObject.GetComponent<MonoBehaviour>().enabled = set;
            foreach (MonoBehaviour s in a.GetComponents<MonoBehaviour>())
                s.enabled = set;
        }
    }
}
