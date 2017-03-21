using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    public bool omni = false;      //bool that checks if you can by all upgrades at one level
    int l1 = 0;
    int l2 = 0;
    int l3 = 0;
    int l4 = 0;
    int l5 = 0;

    public int tier;
    public int upgrade;

    public int l1cost;
    public int l2cost;
    public int l3cost;
    public int l4cost;
    public int l5cost;
    public int omnicost;

    public int level1
    {
        get { return l1; }
        set { l1 = value; }
    }
    public int level2
    {
        get { return l2; }
        set { l2 = value; }
    }
    public int level3
    {
        get { return l3; }
        set { l3 = value; }
    }
    public int level4
    {
        get { return l4; }
        set { l4 = value; }
    }
    public int level5
    {
        get { return l5; }
        set { l5 = value; }
    }

    public void setUpgrade()
    {
        switch (tier)
        {
            case 1:
                if (omni && l1 != 0)
                    l1 = 3;
                else
                    l1 = upgrade;
                PlayerInventory.money -= l1cost;
                break;
            case 2:
                if (omni && l2 != 0)
                    l2 = 3;
                else
                    l2 = upgrade;
                PlayerInventory.money -= l2cost;
                break;
            case 3:
                if (omni && l3 != 0)
                    l3 = 3;
                else
                    l3 = upgrade;
                PlayerInventory.money -= l3cost;
                break;
            case 4:
                if (omni && l4 != 0)
                    l4 = 3;
                else
                    l4 = upgrade;
                PlayerInventory.money -= l4cost;
                break;
            case 5:
                if (omni && l5 != 0)
                    l5 = 3;
                else
                    l5 = upgrade;
                PlayerInventory.money -= l5cost;
                break;
            case 6:
                omni = true;
                PlayerInventory.money -= omnicost;
                break;
            default:
                break;
        }
    }


}