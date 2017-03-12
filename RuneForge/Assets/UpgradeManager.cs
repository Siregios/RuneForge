using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
    bool omni = false;      //bool that checks if you can by all upgrades at one level
    int l1 = 0;
    int l2 = 0;
    int l3 = 0;
    int l4 = 0;
    int l5 = 3;

    public int level1 { get { return l1; }}
    public int level2 { get { return l2; } }
    public int level3 { get { return l3; } }
    public int level4 { get { return l4; } }
    public int level5 { get { return l5; } }

    public void setUpgrade(int temp)
    {
        int tier = temp / 10;
        int upgrade = temp % 10;
        switch (tier)
        {
            case 1:
                if (omni && l1 != 0)
                    l1 = 3;
                else
                    l1 = upgrade;
                break;
            case 2:
                if (omni && l2 != 0)
                    l2 = 3;
                else
                    l2 = upgrade;
                break;
            case 3:
                if (omni && l3 != 0)
                    l3 = 3;
                else
                    l3 = upgrade;
                break;
            case 4:
                if (omni && l4 != 0)
                    l4 = 3;
                else
                    l4 = upgrade;
                break;
            case 5:
                if (omni && l5 != 0)
                    l5 = 3;
                else
                    l5 = upgrade;
                break;
            default:
                break;
        }
    }
}