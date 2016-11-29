using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClipboardUI : MonoBehaviour
{
    public GameObject workOrderButton;
    public float startY = 100;
    public List<WorkOrderButton> buttonList = new List<WorkOrderButton>();
    public GameObject clipboardArea;

    //void OnEnable()
    //{
    //    DisplayBoard();
    //    MasterGameManager.instance.uiManager.uiOpen = true;
    //}

    //void OnDisable()
    //{
    //    MasterGameManager.instance.uiManager.uiOpen = false;
    //}

    //void Awake()
    //{
    //    DisplayBoard();
    //}
    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        if (active)
            DisplayBoard();
    }

    void DisplayBoard()
    {
        ClearButtonList();

        float yPos = startY;
        foreach (WorkOrder order in MasterGameManager.instance.workOrderManager.workorderList)
        {
            GameObject newOrderObject = (GameObject)Instantiate(workOrderButton, clipboardArea.transform.position, Quaternion.identity);
            newOrderObject.transform.SetParent(clipboardArea.transform);
            newOrderObject.transform.localScale = Vector3.one;
            newOrderObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);

            WorkOrderButton newOrderButton = newOrderObject.GetComponent<WorkOrderButton>();
            newOrderButton.Initialize(order);
            yPos -= 50;

            buttonList.Add(newOrderButton);
        }
    }

    void ClearButtonList()
    {
        foreach (WorkOrderButton button in buttonList)
        {
            Destroy(button.gameObject);
        }
        buttonList.Clear();
    }
}
