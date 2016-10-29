using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrderBoardUI : MonoBehaviour {
    public GameObject orderNote;
    [HideInInspector]
    public int currentDisplayedDay = 0;

    RectTransform rectTransform;
    float padX, padY;

    void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        padX = orderNote.GetComponent<RectTransform>().rect.width / 2;
        padY = orderNote.GetComponent<RectTransform>().rect.height / 2;
    }

    public void DisplayBoard()
    {
        this.gameObject.SetActive(true);
        if (currentDisplayedDay == MasterGameManager.instance.actionClock.Day)
            return;

        foreach (Order order in MasterGameManager.instance.orderGenerator.todaysOrders)
        {
            GameObject newOrderNote = (GameObject)Instantiate(orderNote, this.transform.position, Quaternion.identity);
            newOrderNote.transform.SetParent(this.transform);
            newOrderNote.transform.localScale = Vector3.one;

            float xPos = Random.Range(rectTransform.rect.xMin + padX, rectTransform.rect.xMax - padX);
            float yPos = Random.Range(rectTransform.rect.yMin + padY, rectTransform.rect.yMax - padY);
            newOrderNote.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos, 0);

            newOrderNote.transform.FindChild("OrderIcon").GetComponent<Image>().sprite = order.item.icon;
            newOrderNote.transform.FindChild("OrderName").GetComponent<Text>().text = order.item.name;

            newOrderNote.transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));
        }
        currentDisplayedDay = MasterGameManager.instance.actionClock.Day;
    }
}