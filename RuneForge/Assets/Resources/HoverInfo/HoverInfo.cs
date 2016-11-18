using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoverInfo : MonoBehaviour {
    Vector2 BOTTOM_LEFT = new Vector2(0, 0);
    Vector2 BOTTOM_RIGHT = new Vector2(1, 0);
    Vector2 TOP_LEFT = new Vector2(0, 1);
    Vector2 TOP_RIGHT = new Vector2(1, 1);

    public static HoverInfo instance;
    public Text text;
    RectTransform rectTrans;
    float canvasWidth, canvasHeight;
    Vector2 screenCanvasRatio;

    void Awake()
    {
        rectTrans = this.transform.Find("HoverPanel").GetComponent<RectTransform>();
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;
        canvasHeight = canvasRect.rect.height;
        screenCanvasRatio = new Vector2(canvasWidth / Screen.width, canvasHeight / Screen.height);
    }

    public static void Load()
    {
        if (instance == null)
        {
            GameObject newHoverPanel = Instantiate(Resources.Load<GameObject>("HoverInfo/HoverInfo"));
            instance = newHoverPanel.GetComponent<HoverInfo>();
            GameObject tempCanvas = GameObject.Find("Canvas");
            if (tempCanvas != null)
            {
                newHoverPanel.transform.SetParent(tempCanvas.transform);
                newHoverPanel.transform.localScale = Vector3.one;
                newHoverPanel.transform.position = tempCanvas.transform.position;
            }
            else
                Debug.LogError("No Canvas to load HoverInfo Panel");
        }
    }

    public void DisplayItem(ItemButton itemButton)
    {
        text.text = itemButton.item.name;

        StartCoroutine(PositionPanel(itemButton.GetComponent<RectTransform>(), TOP_LEFT, TOP_RIGHT, TOP_RIGHT, TOP_LEFT));
    }

    public void Hide()
    {
        rectTrans.gameObject.SetActive(false);
    }

    Vector2 WorldToCanvasPoint(Vector2 screenPoint)
    {
        float x = screenPoint.x - (Screen.width / 2);
        float y = screenPoint.y - (Screen.height / 2);

        return new Vector2(x * screenCanvasRatio.x, y * screenCanvasRatio.y);
    }

    Vector2 Corner(Vector2 corner, RectTransform rectTransform)
    {
        return WorldToCanvasPoint(rectTransform.position) + new Vector2((corner.x - rectTransform.pivot.x) * rectTransform.rect.width, (corner.y - rectTransform.pivot.y) * rectTransform.rect.height);
    }

    //Tries to attach the panel at a given 'myPivot' to a rectTransform's specified 'objectPivot'. 
    //If the panel goes off the screen, then attaches 'myAltPivot' to the 'objectAltPivot'
    //Coroutine because content fitter component doesn't update until the end of the frame.
    IEnumerator PositionPanel(RectTransform rectTransform, Vector2 myPivot, Vector2 objectPivot, Vector2 myAltPivot, Vector2 objectAltPivot)
    {
        rectTrans.gameObject.SetActive(true);
        rectTrans.GetComponent<CanvasGroup>().alpha = 0;
        rectTrans.pivot = myPivot;
        rectTrans.anchoredPosition = Corner(objectPivot, rectTransform);

        yield return new WaitForEndOfFrame();

        if (myPivot == TOP_LEFT && Corner(TOP_RIGHT, rectTrans).x > (canvasWidth / 2))
        {
            rectTrans.pivot = myAltPivot;
            rectTrans.anchoredPosition = Corner(objectAltPivot, rectTransform);
        }
        rectTrans.GetComponent<CanvasGroup>().alpha = 1;
    }
}
