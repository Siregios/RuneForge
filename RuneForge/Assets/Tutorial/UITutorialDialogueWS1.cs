using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialDialogueWS1 : UITutorialDialogue {

    public int startIndex = 0;
    public int currentIndex;
    public TutorialDialogue dialogueManager;
    public GameObject book;
    public Button recipe;
    public Button clipboard;
    public Button close;

    private void Awake()
    {
        currentIndex = startIndex;
        clipboard.gameObject.GetComponent<Image>().color = clipboard.colors.disabledColor;
        clipboard.enabled = false;
    }

    public override void handleButtonPush(Item itemInfo)
    {
        if(currentIndex == startIndex)
        {
            if (itemInfo.Class == "Rune")
                dialogueManager.ButtonActivate(currentIndex);
            else if (itemInfo.Class == "Product")
                dialogueManager.ButtonActivate(currentIndex + 1);
            currentIndex += 2;
        }

        if(currentIndex == (startIndex + 2))
        {
            
        }
    }

    public void pinOrder()
    {
        if (currentIndex == (startIndex + 2))
        {
            dialogueManager.ActivateDialogue(currentIndex, false);
            currentIndex++;
            dialogueManager.dialogueIndex = currentIndex;
        }
        else if (currentIndex == (startIndex + 3))
        {
            book.GetComponent<BookUI>().Enable(false);
            dialogueManager.ActivateDialogue(currentIndex, false);
            currentIndex++;
            recipe.gameObject.GetComponent<Image>().color = recipe.colors.disabledColor;
            recipe.enabled = false;
            clipboard.enabled = true;
            clipboard.gameObject.GetComponent<Image>().color = clipboard.colors.normalColor;
            dialogueManager.dialogueIndex = currentIndex;
        }
        else if (currentIndex == (startIndex + 4))
        {

        }
    }
}
