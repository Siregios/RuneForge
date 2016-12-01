using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttrBarGroup : MonoBehaviour {
    public AttributeBarUI fireBar, waterBar, earthBar, airBar;
    Dictionary<string, AttributeBarUI> barDict;

    void Awake()
    {
        barDict = new Dictionary<string, AttributeBarUI>()
        {
            { "Fire", fireBar },
            { "Water", waterBar },
            { "Earth", earthBar },
            { "Air", airBar }
        };
        Clear();
    }

    public void SetBar(string bar, int level)
    {
        barDict[bar].SetBar(level);
    }

    public void SetPlaceholder(string bar, int level)
    {
        barDict[bar].SetPlaceholderBar(level);
    }

    public void SetText(string bar, string text)
    {
        barDict[bar].SetText(text);
    }

    public void Clear()
    {
        foreach (var kvp in barDict)
        {
            SetPlaceholder(kvp.Key, 0);
            SetBar(kvp.Key, 0);
            SetText(kvp.Key, "0");
        }
    }
}
