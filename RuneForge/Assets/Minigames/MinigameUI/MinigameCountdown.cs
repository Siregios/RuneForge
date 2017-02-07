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

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        while (count > -1)
        {
            if (count == 3)
                GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime+0.25f);
            if (count == 0)
                GetComponent<Text>().text = "Go!";
            else
                GetComponent<Text>().text = count.ToString();
            count--;
            yield return new WaitForSeconds(1f);
        }
        StartGame(true);
        gameObject.SetActive(false);
    }

    void StartGame(bool set)
    {
        foreach (GameObject a in activate)
        {
            foreach (Transform child in a.transform)
            {
                if (child.GetComponent<MonoBehaviour>() != null && child.GetComponent<Text>() == null)
                    child.GetComponent<MonoBehaviour>().enabled = set;
            }
            foreach (MonoBehaviour s in a.GetComponents<MonoBehaviour>())
                s.enabled = set;
        }
    }
}