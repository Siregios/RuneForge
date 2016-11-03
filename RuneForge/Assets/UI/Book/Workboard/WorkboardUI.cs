using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WorkboardUI : MonoBehaviour {
    public GameObject workOrderButton;
    public float startY = 100;
    List<WorkOrderButton> buttonList = new List<WorkOrderButton>();

    void OnEnable()
    {
        DisplayBoard();
    }

    public void DisplayBoard()
    {
        ClearButtonList();

        float yPos = startY;
        foreach (WorkOrder order in MasterGameManager.instance.workboard.workorderList)
        {
            GameObject newOrderObject = (GameObject)Instantiate(workOrderButton, this.transform.position, Quaternion.identity);
            newOrderObject.transform.SetParent(this.transform);
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
