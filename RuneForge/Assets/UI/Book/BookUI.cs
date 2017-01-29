using UnityEngine;
using System.Collections;

public class BookUI : MonoBehaviour {
    private AudioManager audioManager;
    public enum BookSection
    {
        CLIPBOARD,
        RECIPE
    }
    public BookSection currentSection = BookSection.CLIPBOARD;
    public RecipeUIManager recipe;
    public ClipboardUI clipboard;
    public GameObject menuBar;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.Enable(this.gameObject, active, true);
        //MasterGameManager.instance.uiManager.uiOpen = active;
        //MasterGameManager.instance.interactionManager.canInteract = !active;
        //menuBar.SetActive(!active);
        if (active)
            audioManager.PlaySound(7);
        else
            audioManager.PlaySound(8);
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
        clipboard.Enable(false);
        currentSection = BookSection.RECIPE;
    }

    public void DisplayClipboard()
    {
        Enable(true);
        recipe.Enable(false);
        clipboard.Enable(true);
        currentSection = BookSection.CLIPBOARD;
    }
}
