using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public bool uiOpen = false;
    GameObject menuBar = null;
    GameObject currentUI = null;

    public void Enable(GameObject uiObject, bool active, bool removeMenuBar=false)
    {
        if (active && currentUI != null)
            currentUI.SetActive(false);
        uiObject.SetActive(active);
        currentUI = active ? uiObject : null;
        this.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
        if (removeMenuBar)
        {
            EnableMenuBar(!active);
        }
    }

    public void EnableMenuBar(bool active)
    {
        if (this.menuBar == null)
        {
            menuBar = GameObject.Find("MenuBar");
        }

        this.menuBar.SetActive(active);
    }
}
