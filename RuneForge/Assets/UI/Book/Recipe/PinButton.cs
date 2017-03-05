using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinButton : MonoBehaviour {
    public void OnHover(bool active)
    {
        if (active)
        {
            HoverInfo.Load();
            HoverInfo.instance.DisplayText(this.gameObject, "Pin to Clipboard");
        }
        else
        {
            HoverInfo.instance.Hide();
        }
    }
}
