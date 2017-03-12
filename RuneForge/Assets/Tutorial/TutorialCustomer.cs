using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCustomer : CutsceneMovement {

    public Customer customer;
    public TutorialDialogue dialogueScript;

    public override void cutsceneMove() 
    {
        customer.Leave();
        dialogueScript.ActivateDialogue(dialogueScript.dialogueIndex, true);
    }
}
