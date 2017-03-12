using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialDialogueS1 : UITutorialDialogue {

    public TutorialDialogue dialogueManager;
    public Button questBoard;
    public GameObject Book;
    public Button menuButton;
    public GameObject customer;
    private MasterGameManager gameManager;
    private int bookOpenNum = 0;


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
            }
            if(bookOpenNum == 1)
            {
                currentIndex = index;
                bookOpenNum++;
                dialogueManager.ButtonActivateFalse(currentIndex);
            }
        }

        currentIndex = index;

        if (currentIndex == 6)
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
            questBoard.enabled = false;
            dialogueManager.changeButtonActive(false);
        }

        if(currentIndex == 7) 
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
        }

        if (currentIndex == 8) 
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
            menuButton.interactable = false;
            customer.SetActive(true);
            Book.GetComponent<CloseOnEscape>().enabled = true;   
        }

        if(currentIndex == 9) 
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
            customer.GetComponent<BoxCollider2D>().enabled = true;
        }

        if(currentIndex == 12) 
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
        }
    }
}
