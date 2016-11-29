using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Interactable : MonoBehaviour {
    public UnityEvent MouseClick;
    public UnityEvent MouseHover;
    public UnityEvent MouseExit;

    [HideInInspector]
    public bool hovering = false;

    void Start()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer("Interactable") &&
            this.gameObject.layer != LayerMask.NameToLayer("UI"))
            Debug.LogWarningFormat("'{0}' not on the Interactable layer", this.gameObject.name);
    }

    public void SetGlobalInteract(bool active)
    {
        MasterGameManager.instance.interactionManager.canInteract = active;
    }

    public void EnableGlow()
    {
        GetComponentInChildren<SpriteGlow>().enabled = true;
    }

    public void DisableGlow()
    {
        GetComponentInChildren<SpriteGlow>().enabled = false;
    }
}
