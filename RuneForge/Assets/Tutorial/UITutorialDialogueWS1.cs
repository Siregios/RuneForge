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
        //So the player won't see it in the inventory since the mom "takes it"
        //PlayerInventory.inventory.SetItemCount(ItemCollection.itemDict["Fire Rune (MC)"], 0);
        //PlayerInventory.inventory.SetItemCount(ItemCollection.itemDict["Fire Rune (HQ)"], 0);
        //PlayerInventory.inventory.SetItemCount(ItemCollection.itemDict["Fire Rune"], 0);
        currentIndex = startIndex;
        //clipboard.gameObject.GetComponent<Image>().color = clipboard.colors.disabledColor;
        //clipboard.enabled = false;


    }

    public override void handleButtonPush(Item itemInfo)
    {
        if(currentIndex == startIndex+2)
        {

            //if (itemInfo.Class == "Rune")
            //    dialogueManager.ButtonActivateFalse(currentIndex);
            //else if (itemInfo.Class == "Product")
            //    dialogueManager.ButtonActivateFalse(currentIndex + 1);
            dialogueManager.ButtonActivateFalse(currentIndex+1);
            currentIndex += 1;
            ItemList_ExtraFilter extraFilter = GameObject.Find("ItemListPanel (Product)").GetComponent<ItemList_ExtraFilter>();
            extraFilter.extraFilters[0] = "Water Rune";
            extraFilter.applyExtraFilters();
        }

        //if (currentIndex == (startIndex + 2))
        //{
            
        //}
    }

    public void pinOrder()
    {
        if (currentIndex == (startIndex + 3))
        {            
            currentIndex++;
            dialogueManager.ActivateDialogue(currentIndex, false);
            dialogueManager.dialogueIndex = currentIndex;
        }
        else if (currentIndex == (startIndex + 4))
        {
            //book.GetComponent<BookUI>().Enable(false);
            currentIndex++;
            dialogueManager.ActivateDialogue(currentIndex, false);
            //recipe.gameObject.GetComponent<Image>().color = recipe.colors.disabledColor;
            clipboard.enabled = true;
            dialogueManager.changeButtonActive(false);
            //clipboard.gameObject.GetComponent<Image>().color = clipboard.colors.normalColor;
            dialogueManager.dialogueIndex = currentIndex;
            ItemList_ExtraFilter extraFilter = book.transform.FindChild("Recipe").FindChild("ItemListPanel (Product)").GetComponent<ItemList_ExtraFilter>();
            extraFilter.extraFilters[0] = "None";
            extraFilter.applyExtraFilters();
        }

    }

    public void ButtonActivateOverride(int index)
    {
        currentIndex = index;
        if (currentIndex == startIndex)
        {

            //if (itemInfo.Class == "Rune")
            //    dialogueManager.ButtonActivateFalse(currentIndex);
            //else if (itemInfo.Class == "Product")
            //    dialogueManager.ButtonActivateFalse(currentIndex + 1);
            dialogueManager.ButtonActivateFalse(currentIndex);
            currentIndex += 2;
            recipe.enabled = false;
            dialogueManager.changeButtonActive(false);
            ItemList_ExtraFilter extraFilter = GameObject.Find("ItemListPanel (Product)").GetComponent<ItemList_ExtraFilter>();
            extraFilter.extraFilters[0] = "Earth Rune";

        }
        else if (currentIndex == 14)
        {
            dialogueManager.ButtonActivateTrue(currentIndex+2);
            dialogueManager.dialogueIndex--;
            currentIndex += 2;
            close.enabled = true;
            clipboard.enabled = false;
            dialogueManager.changeButtonActive(false);
        }

        else if (currentIndex == 17)
        {
            dialogueManager.ActivateDialogue(currentIndex, true);
            dialogueManager.dialogueIndex = currentIndex;
            GameObject BlacksmithParent = GameObject.Find("BlacksmithParent");
            BlacksmithParent.transform.FindChild("BlacksmithTools").gameObject.SetActive(false);
            BlacksmithParent.transform.FindChild("Blacksmith").gameObject.SetActive(true);
        }
        //else if (currentIndex == 12)
        //{
        //    dialogueManager.dialogueIndex = currentIndex;
        //    dialogueManager.ActivateDialogue(currentIndex, true);
        //    GameObject BlacksmithParent = GameObject.Find("BlacksmithParent");
        //    BlacksmithParent.transform.FindChild("BlacksmithTools").gameObject.SetActive(false);
        //    BlacksmithParent.transform.FindChild("Blacksmith").gameObject.SetActive(true);
        //}

        //else if (currentIndex == 13)
        //{
        //    dialogueManager.dialogueIndex = currentIndex;
        //    dialogueManager.ActivateDialogue(currentIndex, true);
        //    GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        //}
    }

    //public void disableColor(Button button)
    //{
    //    button.gameObject.GetComponent<Image>().color = button.colors.disabledColor;
    //    button.enabled = false;
    //}
}
