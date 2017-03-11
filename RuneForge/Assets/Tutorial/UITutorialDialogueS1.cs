using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialDialogueS1 : UITutorialDialogue {

    public int startIndex = 0;
    public int currentIndex;
    public TutorialDialogue dialogueManager;
    public Button questBoard;
    private MasterGameManager gameManager;

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


    public void ButtonActivateOverride(int index)
    {
        currentIndex = index;
        if (currentIndex == 3)
        {
            dialogueManager.ButtonActivateFalse(index);
            questBoard.enabled = true;
            dialogueManager.changeButtonActive(false);
        }

        if (currentIndex == 6)
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
            questBoard.enabled = false;
            dialogueManager.changeButtonActive(false);
        }
    }
}
