using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMother : CutsceneMovement {

    public List<GameObject> waypointList;
    public int waypointIndex = 0;
    public TutorialDialogue dialogueScript;
    private PlayerController motherController;

    // Use this for initialization
    void Start () {
        motherController = GetComponent<PlayerController>();
    }
	
	public override void cutsceneMove() {
        if (waypointIndex < waypointList.Count) {
            StartCoroutine(motherMovement());
        }
    }

    IEnumerator motherMovement() {
        dialogueScript.actorsMoving++;
        //Don't judge me
        motherController.enabled = true;
        motherController.movingDirection = (waypointList[waypointIndex].transform.position.x < transform.position.x) ? Direction.DIRECTION.LEFT : Direction.DIRECTION.RIGHT;
        while (Vector2.Distance(transform.position, waypointList[waypointIndex].transform.position) > 0.2f) {
            yield return new WaitForEndOfFrame();
        }
        motherController.movingDirection = Direction.DIRECTION.NONE;
        motherController.enabled = false;
        waypointIndex++;
        dialogueScript.actorsMoving--;
        if (dialogueScript.actorsMoving == 0)
            dialogueScript.ActivateDialogue(dialogueScript.dialogueIndex, true);
    }
}
