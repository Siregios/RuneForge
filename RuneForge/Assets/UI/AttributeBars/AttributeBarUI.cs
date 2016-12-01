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
        bar.sizeDelta = new Vector2(barMaxWidth * ((float)attributeLevel / Item.maxAttributeLevel), bar.rect.height);
    }

    public void SetPlaceholderBar(int level)
    {
        placeholderBar.sizeDelta = new Vector2(barMaxWidth * ((float)level / Item.maxAttributeLevel), bar.rect.height);
    }

    public void SetText(string text)
    {
        level.text = text;
    }
}
