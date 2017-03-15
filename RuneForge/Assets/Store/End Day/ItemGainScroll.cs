using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemGainScroll : MonoBehaviour {
    //x = -105 y = 205 xPad = 70 yPad = 60
    public GameObject itemButton;
    RectTransform viewport;
    public int xDefault =  10, yDefault = -20;
    int xPos, yPos, xPad = 70, yPad = 60;
    int itemCurrentRow = 0;
    int itemMaxRow = 4;
    int additionalRows;
    void Start()
    {
        viewport = GetComponent<RectTransform>();
        xPos = xDefault;
        yPos = yDefault;
        foreach (Item i in MasterGameManager.instance.storeDayStats.inventory.inventoryDict.Keys)
        {
            GameObject item = Instantiate(itemButton) as GameObject;
            item.GetComponent<RectTransform>().position = new Vector2(xPos, yPos);
            xPos += xPad;
            itemCurrentRow++;
            if (itemCurrentRow >= itemMaxRow)
            {
                yPos -= yPad;
                xPos = xDefault;
                itemCurrentRow = 0;
            }
            item.transform.SetParent(transform, false);
            if (Mathf.Abs(item.GetComponent<RectTransform>().localPosition.y) <= 0)
            {
                additionalRows++;
                viewport.sizeDelta = new Vector2(viewport.sizeDelta.x, viewport.sizeDelta.y + 60);
                viewport.localPosition = new Vector2(0, -60 * additionalRows);
            }
        }
    }
}
