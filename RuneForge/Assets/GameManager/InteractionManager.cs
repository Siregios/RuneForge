using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour {
    public bool canInteract = true;
    LayerMask interactableLayer;
    RaycastHit2D hit;

    private Interactable currentInteractable;

    void Awake()
    {
        interactableLayer = 1 << LayerMask.NameToLayer("Interactable");
    }

    void Update()
    {
        canInteract = !MasterGameManager.instance.uiManager.uiOpen;
        if (canInteract)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, 1, interactableLayer);
            if (hit)
            {
                Interactable interactScript = hit.collider.GetComponent<Interactable>();
                if (currentInteractable != null && currentInteractable != interactScript)
                {
                    currentInteractable.MouseExit.Invoke();
                }
                currentInteractable = interactScript;
                currentInteractable.MouseHover.Invoke();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentInteractable.MouseClick.Invoke();
                    currentInteractable.MouseExit.Invoke();
                }
            }
            else if (currentInteractable != null)
            {
                currentInteractable.MouseExit.Invoke();
                currentInteractable = null;
            }
        }
    }
}
