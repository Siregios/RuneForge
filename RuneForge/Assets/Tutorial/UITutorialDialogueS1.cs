using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialDialogueS1 : UITutorialDialogue {

    public TutorialDialogue dialogueManager;
    public Button questBoard;
    public Button recipe;
    public GameObject Book;
    public Button menuButton;
    public Button recipeBackButton;
    public Button recipePinButton;
    public Button shopBuyButton;
    public GameObject customer;
    public GameObject shopkeep;
    public ItemListUI itemList;
    private MasterGameManager gameManager;
    private int bookOpenNum = 0;
    private int closeBookNum = 0;


    private void Awake()
    {
        currentIndex = startIndex;
        gameManager = MasterGameManager.instance;
        gameManager.questGenerator.currentQuests.Add(new Quest(ItemCollection.itemDict["Earth Rune"], 1));
        gameManager.questGenerator.currentQuests.Add(new Quest(ItemCollection.itemDict["Water Rune"], 1));
    }

    public override void handleButtonPush(Item itemInfo)
    {
        if(itemInfo.name == "Enchanted Weapon")
        {
            ButtonActivateOverride(18);
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
            currentIndex = index;
            dialogueManager.ButtonActivateFalse(currentIndex);
            recipe.enabled = false;
            dialogueManager.changeButtonActive(false);
            ItemCollection.itemDict["Enchanted Weapon"].level = 1;
            ItemCollection.itemDict["Enchanted Weapon"].requiredAttributes["Fire"] = 3;
            recipePinButton.enabled = false;
            //StartCoroutine(StupidClumsyFix());
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
            Book.GetComponent<CloseOnEscape>().enabled = false;
            dialogueManager.ButtonActivateFalse(currentIndex);
        }

        if(index == 27)
        {

        }
    }

    //IEnumerator StupidClumsyFix() {
    //    yield return new WaitForEndOfFrame();
    //    itemList.DisplayNewFilter("Enchanted Weapon");
    //}
}
