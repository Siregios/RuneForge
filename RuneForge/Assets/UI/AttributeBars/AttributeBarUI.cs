using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributeBarUI : MonoBehaviour {
    public Text level;
    public RectTransform bar, placeholderBar;
    float barMaxWidth;

    void Awake()
    {
        barMaxWidth = bar.rect.width;
    }

    public void SetBar(int attributeLevel)
    {
        float barWidth = Mathf.Min(barMaxWidth * ((float)attributeLevel / Item.maxAttributeLevel), barMaxWidth);
        bar.sizeDelta = new Vector2(barWidth, bar.rect.height);
    }

    public void SetPlaceholderBar(int level)
    {
        float barWidth = Mathf.Min(barMaxWidth * ((float)level / Item.maxAttributeLevel), barMaxWidth);
        placeholderBar.sizeDelta = new Vector2(barWidth, bar.rect.height);
    }

    public void SetText(string text)
    {
        level.text = text;
    }
}
