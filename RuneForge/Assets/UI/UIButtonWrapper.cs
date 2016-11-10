using UnityEngine;
using System.Collections;

public class UIButtonWrapper : MonoBehaviour {
    public void Deactivate(GameObject toDeactivate)
    {
        if (toDeactivate != null)
            toDeactivate.SetActive(false);
        else
            Debug.Log("Nothing to deactivate");
    }

    public void SetGlobalUIOpen(bool isOpen)
    {
        MasterGameManager.instance.uiManager.uiOpen = isOpen;
    }

    public void SetGlobalInteractable(bool canInteract)
    {
        MasterGameManager.instance.interactionManager.canInteract = canInteract;
    }
}
