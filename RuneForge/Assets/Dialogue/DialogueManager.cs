using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public GameObject dialogueUI;
    public DialogueHandler dialogueScript;

    void Start()
    {
        dialogueScript = GameObject.FindGameObjectWithTag("DialogueUI").GetComponent<DialogueHandler>();
        dialogueUI = dialogueScript.dialogueUI;
        
        dialogueUI.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            dialogueUI.SetActive(true);
            dialogueScript.GetComponent<DialogueHandler>().LoadTextAsset(0);
            dialogueScript.enabled = true;
            dialogueScript.SetBackground("Temp");
        }
    }
}
