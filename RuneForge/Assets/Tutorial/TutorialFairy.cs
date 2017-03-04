using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFairy : CutsceneMovement
{


    //Locations of fairy
    public List<GameObject> waypointList;
    public int waypointIndex = 0;
    public TutorialDialogue dialogueScript;
    float defaultWidth = Screen.width;
    float defaultHeight = Screen.height;
    float speed = ((Screen.width + Screen.height) / 5.5f);

    void Update()
    {
        if (Screen.width != defaultWidth || Screen.height != defaultHeight)
        {
            speed = ((Screen.width + Screen.height) / 5.5f);
            defaultWidth = Screen.width;
            defaultHeight = Screen.height;
        }
    }

    public override void cutsceneMove()
    {
        if (waypointIndex < waypointList.Count)
        {
            StartCoroutine(fairyMovement());
        }
    }

    IEnumerator fairyMovement()
    {
        dialogueScript.actorsMoving++;
        while (Vector2.Distance(transform.position, waypointList[waypointIndex].transform.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypointList[waypointIndex].transform.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        waypointIndex++;
        dialogueScript.actorsMoving--;
        if(dialogueScript.actorsMoving == 0)
            dialogueScript.ActivateDialogue(dialogueScript.dialogueIndex, true);
    }
}
