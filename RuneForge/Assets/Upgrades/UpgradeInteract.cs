using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInteract : MonoBehaviour {
    public int level;
	// Update is called once per frame
	void Update () {
        if(MasterGameManager.instance.playerStats.level < level)
            gameObject.GetComponent<Button>().interactable = false;
        else
            gameObject.GetComponent<Button>().interactable = true;
    }
}
