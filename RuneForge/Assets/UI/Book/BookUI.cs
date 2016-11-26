using UnityEngine;
using System.Collections;

public class BookUI : MonoBehaviour {
    public enum BookSection
    {
        CLIPBOARD,
        RECIPE
    }
    public BookSection currentSection = BookSection.CLIPBOARD;
    public RecipeUIManager recipe;
    public GameObject clipboard;
    public GameObject menuBar;

    void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
        menuBar.SetActive(!active);
    }

    public void DisplaySection(BookSection section)
    {
        switch (section)
        {
            case BookSection.CLIPBOARD:
                DisplayRecipe();
                break;
            case BookSection.RECIPE:
                DisplayClipboard();
                break;
        }
    }

    public void DisplayRecipe()
    {
        Enable(true);
        recipe.Enable(true);
        clipboard.SetActive(false);
        currentSection = BookSection.RECIPE;
    }

    public void DisplayClipboard()
    {
        Enable(true);
        recipe.Enable(false);
        clipboard.SetActive(true);
        currentSection = BookSection.CLIPBOARD;
    }
}
