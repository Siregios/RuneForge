using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuestBoardUI : QuestBoardUI {


    public int currentIndex;
    public UITutorialDialogue UIDialogueHandler;

    public override void turnInQuest(GameObject quest, GameObject item) {
        currentIndex = UIDialogueHandler.currentIndex;
        base.turnInQuest(quest, item);
        UIDialogueHandler.ButtonActivateOverride(++currentIndex);
        PlayerInventory.inventory.SetItemCount(ItemCollection.itemDict["Sword"], 0);


    }
}
