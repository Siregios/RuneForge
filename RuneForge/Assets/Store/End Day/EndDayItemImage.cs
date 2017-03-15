using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayItemImage : MonoBehaviour {
    public Item item;

    public void OnHover(bool active)
    {
        if (active)
        {
            HoverInfo.Load();
            HoverInfo.instance.DisplayText(this.gameObject, item.name);
        }
        else
        {
            HoverInfo.instance.Hide();
        }
    }
}