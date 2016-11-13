using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;

public class RuneChargeUI : MonoBehaviour {

    private ChargeSelector selectorInfo;

    public Text timeInfo;   //The text used for the time left in each mode, also tells which mode game is currently in
    public Text scoreInfo;  //The text used for all scoring information

    public GameObject selectorObj;

	// Use this for initialization
	void Start () {
        selectorInfo = selectorObj.GetComponent<ChargeSelector>();
        timeInfo.text = String.Format("{0}!\n{1:0}", selectorInfo.getMode(), selectorInfo.getTimeRemaining());
        scoreInfo.text = String.Format("Score: {0:0}", selectorInfo.getScore());

    }
	
	// Update is called once per frame
	void Update () {
        timeInfo.text = String.Format("{0}!\n{1:0}", selectorInfo.getMode(), selectorInfo.getTimeRemaining());
        scoreInfo.text = String.Format("Score: {0:0}", selectorInfo.getScore());

    }
}
