using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookUI : MonoBehaviour {
    private AudioManager audioManager;
    public enum BookSection
    {
        INVENTORY,
        RECIPE,
        CLIPBOARD,
        QUESTBOARD,
        SETTINGS
    }
    public BookSection currentSection = BookSection.INVENTORY;
    public GameObject inventory;
    public RecipeUIManager recipe;
    public ClipboardUI clipboard;
    public QuestBoardUI questboard;
    public GameObject menuBar;
    public Text moneyText;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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

    public void DisplayInventory()
    {
        Enable(true);
        inventory.gameObject.SetActive(true);
        recipe.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        currentSection = BookSection.INVENTORY;
    }

    public void DisplayRecipe()
    {
        Enable(true);
        recipe.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        currentSection = BookSection.RECIPE;
    }

    public void DisplayClipboard()
    {
        Enable(true);
        clipboard.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        questboard.gameObject.SetActive(false);
        currentSection = BookSection.CLIPBOARD;
    }

    public void DisplayQuestboard()
    {
        Enable(true);
        questboard.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
        recipe.gameObject.SetActive(false);
        clipboard.gameObject.SetActive(false);
        currentSection = BookSection.QUESTBOARD;
    }
}
