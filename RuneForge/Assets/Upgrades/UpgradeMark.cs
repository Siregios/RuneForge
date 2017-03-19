using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMark : MonoBehaviour {
    public int num;
    int tier;
    int upgrade;
	// Update is called once per frame
	void Update () {
        tier = num / 10;
        upgrade = num % 10;
        switch (tier)
        {
            case 1:
                gameObject.GetComponent<Image>().enabled = (MasterGameManager.instance.upgradeManager.level1 == upgrade || MasterGameManager.instance.upgradeManager.level1 == 3);
                break;
            case 2:
                gameObject.GetComponent<Image>().enabled = (MasterGameManager.instance.upgradeManager.level2 == upgrade || MasterGameManager.instance.upgradeManager.level2 == 3);
                break;
            case 3:
                gameObject.GetComponent<Image>().enabled = (MasterGameManager.instance.upgradeManager.level3 == upgrade || MasterGameManager.instance.upgradeManager.level3 == 3);
                break;
            case 4:
                gameObject.GetComponent<Image>().enabled = (MasterGameManager.instance.upgradeManager.level4 == upgrade || MasterGameManager.instance.upgradeManager.level4 == 3);
                break;
            case 5:
                gameObject.GetComponent<Image>().enabled = (MasterGameManager.instance.upgradeManager.level5 == upgrade || MasterGameManager.instance.upgradeManager.level5 == 3);
                break;
            case 6:
                gameObject.GetComponent<Image>().enabled = (MasterGameManager.instance.upgradeManager.omni);
                break;
        } 
	}
}
