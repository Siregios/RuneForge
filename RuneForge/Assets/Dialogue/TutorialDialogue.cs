using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{

    public GameObject dialogueUI;
    public DialogueHandler dialogueScript;
    public List<Button> menuButtons = new List<Button>();
    public List<int> movementIndex = new List<int>();
    public TutorialFairy fairyScript;
    public int dialogueIndex = 0;
    bool disableOnce = false;

    void Start()
    {
        dialogueScript = GameObject.FindGameObjectWithTag("DialogueUI").GetComponent<DialogueHandler>();
        dialogueUI = dialogueScript.dialogueUI;

        dialogueUI.SetActive(false);
        ActivateDialogue(dialogueIndex, true);
        MasterGameManager.instance.workOrderManager.CreateWorkOrder(ItemCollection.itemDict["Fire Rune"], false, false);
    }

    void Update()
    {
        if (dialogueUI.activeSelf == false && !fairyScript.isMoving)
        {
            if (movementIndex.Contains(dialogueIndex))
            {
                dialogueIndex++;
                fairyScript.moveFairy();
            }
            else if (disableOnce)
            {
                MasterGameManager.instance.inputActive = true;
                MasterGameManager.instance.uiManager.uiOpen = false;
                foreach (Button button in menuButtons)
                {
                    button.interactable = true;
                }
                disableOnce = false;
            }
        }
    }


    public void ActivateDialogue(int index, bool disableAfter)
    {
        disableOnce = disableAfter;
        MasterGameManager.instance.inputActive = false;
        MasterGameManager.instance.uiManager.uiOpen = true;
        foreach (Button button in menuButtons)
        {
            button.interactable = false;
        }
        dialogueUI.SetActive(true);
        dialogueScript.GetComponent<DialogueHandler>().LoadTextAsset(index);
        dialogueScript.enabled = true;
        dialogueScript.SetBackground("");
    }

    //Button Use
    public void ButtonActivate(int index)
    {
        ActivateDialogue(index, false);
    }
}
