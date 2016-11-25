using UnityEngine;
using System.Collections;

public class BookUI : MonoBehaviour {
    public enum BookSection
    {
        CLIPBOARD,
        RECIPE
    }
    public BookSection currentDisplay = BookSection.CLIPBOARD;
    public GameObject recipe, clipboard;
    public GameObject menuBar;

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
        menuBar.SetActive(!active);
    }

    public void ClickTab(BookSection section)
    {
        switch (section)
        {
            case BookSection.CLIPBOARD:
                break;
            case BookSection.RECIPE:
                break;
        }
    }

    public void DisplayRecipe()
    {
        Enable(true);
        recipe.SetActive(true);
        clipboard.SetActive(false);
    }

    public void DisplayClipboard()
    {
        Enable(true);
        recipe.SetActive(false);
        clipboard.SetActive(true);
    }
}
