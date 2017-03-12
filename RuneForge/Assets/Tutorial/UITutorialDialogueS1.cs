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
    public GameObject customer;
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
            }

        }

        if(index == 12) 
        {
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
            menuButton.gameObject.SetActive(true);
        }

        if (index == 17) 
        {
            Debug.Log("BAM");
            currentIndex = index;
            dialogueManager.ButtonActivateTrue(currentIndex);
            recipe.enabled = false;
            dialogueManager.changeButtonActive(false);
            ItemCollection.itemDict["Enchanted Weapon"].level = 1;
            StartCoroutine(StupidClumsyFix());
        }
    }

    IEnumerator StupidClumsyFix() {
        yield return new WaitForEndOfFrame();
        itemList.DisplayNewFilter("Enchanted Weapon");
    }
}
