using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookUI : MonoBehaviour {
    public enum BookSection
    {
        FRONT_PAGE,
        INVENTORY,
        RECIPE,
        CLIPBOARD,
        QUESTBOARD,
        UPGRADES
    }
    public BookSection currentSection = BookSection.INVENTORY;
    public FrontPageUIManager frontPage;
    public GameObject inventory;
    public RecipeUIManager recipe;
    public ClipboardUI clipboard;
    public QuestBoardUI questboard;
    public GameObject upgrades;
    public GameObject menuBar;
    public Text moneyText;
    public AudioClip bookClose;
    public AudioClip bookOpen;

    void Awake()
    {

    }

    void Update()
    {
        moneyText.text = PlayerInventory.money.ToString();
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.Enable(this.gameObject, active, true);
        //MasterGameManager.instance.uiManager.uiOpen = active;
        //MasterGameManager.instance.interactionManager.canInteract = !active;
        //menuBar.SetActive(!active);
        if (active)
            MasterGameManager.instance.audioManager.PlaySFXClip(bookOpen);
        else
            MasterGameManager.instance.audioManager.PlaySFXClip(bookClose);
    }

    //public void DisplaySection(BookSection section)
    //{
    //    switch (section)
    //    {
    //        case BookSection.CLIPBOARD:
    //            DisplayRecipe();
    //            break;
    //        case BookSection.RECIPE:
    //            DisplayClipboard();
    //            break;
    //    }
    //}

    public void DisplayFrontPage()
    {
        Enable(true);
        frontPage.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(false);
        currentSection = BookSection.FRONT_PAGE;
    }

    public void DisplayInventory()
    {
        Enable(true);
        inventory.gameObject.SetActive(true);
        frontPage.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(false);
        currentSection = BookSection.INVENTORY;
    }

    public void DisplayRecipe()
    {
        Enable(true);
        recipe.gameObject.SetActive(true);
        frontPage.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(false);
        currentSection = BookSection.RECIPE;
    }

    public void DisplayClipboard()
    {
        Enable(true);
        clipboard.gameObject.SetActive(true);
        frontPage.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(false);
        currentSection = BookSection.CLIPBOARD;
    }

    public void DisplayQuestboard()
    {
        Enable(true);
        questboard.gameObject.SetActive(true);
        frontPage.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(false);
        currentSection = BookSection.QUESTBOARD;
    }

    public void DisplayUpgrades()
    {
        Enable(true);
        upgrades.gameObject.SetActive(true);
        frontPage.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        currentSection = BookSection.UPGRADES;
    }
}
