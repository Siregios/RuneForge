using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : CutsceneMovement {


    public List<GameObject> waypointList;
    public int waypointIndex = 0;
    public TutorialDialogue dialogueScript;
    private PlayerController playerController;

    void Start() {
        playerController = GetComponent<PlayerController>();
    }

    public override void cutsceneMove() {
        if (waypointIndex < waypointList.Count) {
            StartCoroutine(playerMovement());
        }
    }

    IEnumerator playerMovement() {
        dialogueScript.actorsMoving++;
        //Don't judge me
        playerController.movingDirection = (waypointList[waypointIndex].transform.position.x < transform.position.x) ? Direction.DIRECTION.LEFT : Direction.DIRECTION.RIGHT;
        while (Vector2.Distance(transform.position, waypointList[waypointIndex].transform.position) > 0.2f) {
            yield return new WaitForEndOfFrame();
        }
        playerController.movingDirection = Direction.DIRECTION.NONE;
        waypointIndex++;
        dialogueScript.actorsMoving--;
        if(dialogueScript.actorsMoving == 0)
            dialogueScript.ActivateDialogue(dialogueScript.dialogueIndex, true);
    }
}
