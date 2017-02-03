using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{

    public GameObject dialogueUI;
    public DialogueHandler dialogueScript;
    public List<Button> menuButtons = new List<Button>();   //List of all interactable menu buttons

    public List<int> movementIndex = new List<int>();       //List of which dialoguee indexes initiate movement

    public List<string> characterMovments;                  //List for which characters we want moved in what order, separated by commas (you could also make
                                                            //this a double list, but I think it saves more space making that second list locally later on)

    private Dictionary<int, string> movementDict = new Dictionary<int, string>();       //Dictionary of movement indexes corresponding to which characters move on that index
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
        MasterGameManager.instance.workOrderManager.workorderList[0].requiredStages = 1;

        for (int i = 0; i<movementIndex.Capacity; i++)
        {
            movementDict[movementIndex[i]] = characterMovments[i];
        }
    }

    void Update()
    {
        if (dialogueUI.activeSelf == false && !fairyScript.isMoving)
        {
            if (movementIndex.Contains(dialogueIndex))
            {
                string[] charactersToMove = movementDict[dialogueIndex].Replace(" ", string.Empty).Split(',');
                foreach (string character in charactersToMove)
                {
                    CutsceneMovement movementScript = GameObject.Find(character).GetComponent<CutsceneMovement>();
                    movementScript.cutsceneMove();
                }
                dialogueIndex++;
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
                dialogueIndex++;
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
