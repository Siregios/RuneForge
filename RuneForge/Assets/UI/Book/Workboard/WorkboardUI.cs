using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WorkboardUI : MonoBehaviour {
    public GameObject workOrderButton;
    public float startY = +100;
    List<WorkOrderButton> buttonList = new List<WorkOrderButton>();

    public void DisplayBoard(bool interactableButtons)
    {
        this.transform.parent.gameObject.SetActive(true);

        buttonList.Clear();
        float yPos = startY;
        foreach (WorkOrder order in MasterGameManager.instance.workboard.workorderList)
        {
            GameObject newOrderObject = (GameObject)Instantiate(workOrderButton, this.transform.position, Quaternion.identity);
            newOrderObject.transform.SetParent(this.transform);
            newOrderObject.transform.localScale = Vector3.one;
            newOrderObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);

            WorkOrderButton newOrderButton = newOrderObject.GetComponent<WorkOrderButton>();
            newOrderButton.Initialize(order);
            buttonList.Add(newOrderButton);
            yPos -= 50;
        }

        SetButtonsInteractable(interactableButtons);
    }

    public void SetButtonsInteractable(bool interactable)
    {
        foreach (WorkOrderButton orderButton in buttonList)
        {
            orderButton.GetComponent<Button>().interactable = interactable;
        }
    }
}
