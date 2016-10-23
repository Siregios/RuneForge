using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This should be attached to "FilterInvButton" prefabs.
/// 
/// It will pass a FilterString to the InventoryListUI which should be attached to this button's parent.
/// 
/// It should have a child that is named "FilterWord". That child should also have a child whose name is the string to filter by.
/// </summary>
public class FilterInventoryButton : MonoBehaviour {
    InventoryListUI invListUI;

    [HideInInspector]
    public string filterString;

    [HideInInspector]
    public bool isPressed = false;

    Image image;

    void Awake()
    {
        invListUI = this.transform.parent.GetComponent<InventoryListUI>();
        if (invListUI == null)
        {
            Debug.LogError("FilterButton not a child of InventoryListUI");
        }

        filterString = this.transform.FindChild("FilterWord").GetChild(0).name;
        image = this.GetComponent<Image>();
    }

    public void Release()
    {
        image.color = Color.white;
        isPressed = false;
    }

    public void OnClick()
    {
        if (!isPressed)
        {
            image.color = Color.grey;
            isPressed = true;
        }
        else
        {
            Release();
        }

        invListUI.ClickFilterButton(this);
    }
}
