using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialDialogueS1 : UITutorialDialogue {

    public int startIndex = 0;
    public int currentIndex;
    public TutorialDialogue dialogueManager;
    public Button questBoard;

    private void Awake()
    {
        currentIndex = startIndex;
    }

    public override void handleButtonPush(Item itemInfo)
    {

    }


    public void ButtonActivateOverride(int index)
    {
        currentIndex = index;
        if (currentIndex == 3)
        {
            questBoard.enabled = true;
            dialogueManager.changeButtonActive(false);
        }

        if (currentIndex == 4)
        {
            dialogueManager.ButtonActivateTrue(currentIndex);
        }
    }
}
