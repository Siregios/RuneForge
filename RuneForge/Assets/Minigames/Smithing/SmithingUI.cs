using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;

public class SmithingUI : MonoBehaviour {

    private ChargeSelector selectorInfo;

    public Timer timer;


    public Image clockPointer;

    public Image clockFill;

    public GameObject selectorObj;

	// Use this for initialization
	void Start () {
        selectorInfo = selectorObj.GetComponent<ChargeSelector>();
    }
	
	// Update is called once per frame
	void Update () {

        if (timer.timeEnd)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }
        string mode = selectorInfo.getMode();
        if (mode == "Fire") 
        {
            clockPointer.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 360 * selectorInfo.getTimeRemaining() / selectorInfo.chargeTime));
            clockFill.fillAmount = 1 - (selectorInfo.getTimeRemaining() / selectorInfo.chargeTime);
            if (selectorInfo.getTimeRemaining() <= 0)
                clockFill.fillAmount = 0;
        } 
        else 
        {
            clockFill.fillAmount = 0;
        }

        //rotation 360*currenttime/maxtime

    }
}
