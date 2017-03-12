using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour {



    public void setUpgrade(int temp)
    {
        MasterGameManager.instance.upgradeManager.setUpgrade(temp);
    }
}
