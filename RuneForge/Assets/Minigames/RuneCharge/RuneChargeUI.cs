using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;

public class RuneChargeUI : MonoBehaviour {

    private ChargeSelector selectorInfo;

    public float timer = 30f;
    public Text timerText;

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
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();

        if (timer < 0.01f)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }
        timeInfo.text = String.Format("{0}!\n{1:0}", selectorInfo.getMode(), selectorInfo.getTimeRemaining());
        scoreInfo.text = String.Format("Score: {0:0}", selectorInfo.getScore());

    }
}
