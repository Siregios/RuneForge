using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoverInfo : MonoBehaviour {
    public static HoverInfo instance;
    public Text text;
    RectTransform rectTrans;
    float canvasWidth, canvasHeight;
    Vector2 screenCanvasRatio;

    void Awake()
    {
        rectTrans = this.GetComponent<RectTransform>();
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;
        canvasHeight = canvasRect.rect.height;
        screenCanvasRatio = new Vector2(canvasWidth / Screen.width, canvasHeight / Screen.height);
    }

    public static void Load()
    {
        if (instance == null)
        {
            GameObject newHoverPanel = Instantiate(Resources.Load<GameObject>("HoverInfo/HoverPanel"));
            instance = newHoverPanel.GetComponent<HoverInfo>();
            GameObject tempCanvas = GameObject.Find("Canvas");
            if (tempCanvas != null)
                newHoverPanel.transform.SetParent(tempCanvas.transform);
            else
                Debug.LogError("No Canvas to load HoverInfo Panel");
        }
    }

    public void DisplayItem(ItemButton itemButton)
    {
        text.text = itemButton.item.name;
        rectTrans.pivot = new Vector2(0, 1);
        rectTrans.anchoredPosition = ScreenToCanvasPoint(itemButton.transform.position);
    }

    Vector2 ScreenToCanvasPoint(Vector2 screenPoint)
    {
        float x = screenPoint.x - (Screen.width / 2);
        float y = screenPoint.y - (Screen.height / 2);

        return new Vector2(x * screenCanvasRatio.x, y * screenCanvasRatio.y);
    }
}
