using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialDialogueS1 : UITutorialDialogue {

    public TutorialDialogue dialogueManager;
    public Button questBoard;
    public Button recipe;
    public GameObject Book;
    public GameObject shopUI;
    public Button menuButton;
    public Button recipeBackButton;
    public Button recipePinButton;
    public Button shopBuyButton;
    public GameObject customer;
    public GameObject shopkeep;
    public GameObject stopWall;
    public GameObject Player;
    public ItemListUI itemList;
    private MasterGameManager gameManager;
    private bool swordBool = true;
    private bool runeBool = true;
    private int bookOpenNum = 0;
    private int closeBookNum = 0;
    private int recipeOpenNum = 0;
    private int componentNum = 0;


    private void Awake()
    {
        currentIndex = startIndex;
        gameManager = MasterGameManager.instance;
        gameManager.questGenerator.currentQuests.Add(new Quest(ItemCollection.itemDict["Water Rune"], 1));
        gameManager.questGenerator.currentQuests.Add(new Quest(ItemCollection.itemDict["Earth Rune"], 1));
        PlayerInventory.inventory.SetItemCount(ItemCollection.itemDict["Air Rune"], 0);
    }

    public override void handleButtonPush(Item itemInfo)
    {
        if(itemInfo.name == "Enchanted Weapon" && currentIndex < 18)
        {
            ButtonActivateOverride(18);
        }

        if(itemInfo.name == "Sword" && currentIndex >= 30 && swordBool == true)
        {
            swordBool = false;
            componentNum++;
            ButtonActivateOverride(32);
        }

        if (itemInfo.icon.name == "Fire Rune" && currentIndex >= 30 && runeBool == true)
        {
            runeBool = false;
            componentNum++;
            ButtonActivateOverride(32);
        }
    }


    public override void ButtonActivateOverride(int index)
    {
        if (index == 3)
        {
            if (bookOpenNum == 0) 
            {
                currentIndex = index;
                bookOpenNum++;
                dialogueManager.ButtonActivateFalse(currentIndex);
                questBoard.enabled = true;
                dialogueManager.changeButtonActive(false);
                return;
            }
            if(bookOpenNum == 1)
            {
                currentIndex += 2;
                bookOpenNum++;
                dialogueManager.ButtonActivateFalse(currentIndex);
                recipe.enabled = true;
                dialogueManager.changeButtonActive(false);
                return;
            }
            if(bookOpenNum == 2)
            {
                currentIndex = 29;
                bookOpenNum++;
                dialogueManager.ButtonActivateFalse(currentIndex);
                recipe.enabled = true;
                dialogueManager.changeButtonActive(true);
                return;
            }
        }


        if (index == 6)
        {
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
            questBoard.enabled = false;
            dialogueManager.changeButtonActive(false);
        }

        if(index == 7) 
        {
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
        }

        if (index == 8) 
        {
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
            menuButton.gameObject.SetActive(false);
            customer.SetActive(true);
            Book.GetComponent<CloseOnEscape>().enabled = true;   
        }

        if(index == 9) 
        {
            if(closeBookNum == 0) 
            {
                currentIndex = index;
                closeBookNum++;
                dialogueManager.ButtonActivateTrue(currentIndex);
                customer.GetComponent<BoxCollider2D>().enabled = true;
                return;
            }

            if(closeBookNum == 1)
            {
                currentIndex = 22;
                closeBookNum++;
                dialogueManager.ButtonActivateTrue(currentIndex);
                menuButton.gameObject.SetActive(false);
                return;
            }

        }

        if(index == 12) 
        {
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
            menuButton.gameObject.SetActive(true);
            Book.GetComponent<CloseOnEscape>().enabled = false;
        }

        if (index == 17) 
        {
            if (recipeOpenNum == 0)
            {
                recipeOpenNum++;
                currentIndex = index;
                dialogueManager.ButtonActivateFalse(currentIndex);
                recipe.enabled = false;
                //dialogueManager.changeButtonActive(false);
                ItemCollection.itemDict["Enchanted Weapon"].level = 1;
                ItemCollection.itemDict["Enchanted Weapon"].requiredAttributes["Fire"] = 3;
                recipePinButton.enabled = false;
                //StartCoroutine(StupidClumsyFix());
                return;
            }
            if(recipeOpenNum == 1)
            {
                recipeOpenNum++;
                currentIndex = 31;
                return;
            }

        }

        if(index == 18)
        {
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
            recipeBackButton.enabled = false;
            Book.GetComponent<CloseOnEscape>().enabled = true;
            shopkeep.SetActive(true);
        }

        if(index == 24)
        {
            currentIndex = index;
            shopUI.GetComponent<CloseOnEscape>().enabled = false;
            dialogueManager.ButtonActivateFalse(currentIndex);
        }

        if(index == 27)
        {
            currentIndex = index;
            shopBuyButton.enabled = false;
            shopUI.GetComponent<CloseOnEscape>().enabled = true;
            dialogueManager.ButtonActivateFalse(currentIndex);
            menuButton.gameObject.SetActive(true);
            //menuButton.interactable = true; //idk why this doesn't work
        }

        if(index == 28)
        {
            currentIndex = index;
            shopkeep.GetComponent<Interactable>().enabled = false;
            dialogueManager.ButtonActivateFalse(currentIndex);
            menuButton.interactable = true; //idk why this fixes it
            Book.GetComponent<CloseOnEscape>().enabled = false;

        }

        if(index == 31)
        {
            currentIndex = index;
            dialogueManager.ButtonActivateFalse(currentIndex);
        }

        if(index == 32)
        {
            if(componentNum == 1)
            {
                currentIndex = 32;
                dialogueManager.ButtonActivateFalse(currentIndex);
                return;
            }
            if(componentNum == 2)
            {
                currentIndex = 33;
                dialogueManager.ButtonActivateFalse(currentIndex);
                recipePinButton.enabled = true;
                
            }
        }

        if(index == 34)
        {
            currentIndex = index;
            dialogueManager.ButtonActivateFalse(currentIndex);
            stopWall.SetActive(false);
            Player.GetComponent<PlayerController>().enabled = true;
            Book.GetComponent<CloseOnEscape>().enabled = true;
            Book.GetComponent<CloseOnEscape>().OnEscape.Invoke();
            menuButton.gameObject.SetActive(false);

            MasterGameManager.instance.uiManager.uiOpen = false;
            MasterGameManager.instance.inputActive = true;
        }
    }

    //IEnumerator StupidClumsyFix() {
    //    yield return new WaitForEndOfFrame();
    //    itemList.DisplayNewFilter("Enchanted Weapon");
    //}
}
