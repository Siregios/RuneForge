﻿using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;

public class RuneChargeUI : MonoBehaviour {

    private ChargeSelector selectorInfo;

    public Timer timer;

    public Text timeInfo;   //The text used for the time left in each mode, also tells which mode game is currently in

    public GameObject selectorObj;

	// Use this for initialization
	void Start () {
        selectorInfo = selectorObj.GetComponent<ChargeSelector>();
        timeInfo.text = String.Format("{0}!\n{1:0}", selectorInfo.getMode(), selectorInfo.getTimeRemaining());
    }
	
	// Update is called once per frame
	void Update () {

        if (timer.timeEnd)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }
        timeInfo.text = String.Format("{0}!\n{1:0}", selectorInfo.getMode(), selectorInfo.getTimeRemaining());

    }
}
