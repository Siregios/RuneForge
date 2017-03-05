using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTab : MonoBehaviour {
    public string tabName;
    public void OnHover(bool active)
    {
        if (active)
        {
            HoverInfo.Load();
            HoverInfo.instance.DisplayText(this.gameObject, tabName);
        }
        else
        {
            HoverInfo.instance.Hide();
        }
    }
}
