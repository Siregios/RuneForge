using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialDialogueWS1 : UITutorialDialogue {

    public int startIndex = 0;
    public int currentIndex;
    public TutorialDialogue dialogueManager;

    private void Awake()
    {
        currentIndex = startIndex;
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
}
