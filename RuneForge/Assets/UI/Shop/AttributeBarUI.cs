using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributeBarUI : MonoBehaviour {
    public Text level;
    public RectTransform bar;
    float barMaxWidth;

    void Awake()
    {
        barMaxWidth = bar.rect.width;
    }

    public void RefreshBar(int attributeLevel)
    {
        level.text = attributeLevel.ToString();
        bar.sizeDelta = new Vector2(barMaxWidth * ((float)attributeLevel / Item.maxAttributeLevel), bar.rect.height);
    }
}
