using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof (Collider2D))]
public class Interactable : MonoBehaviour {
    LayerMask interactableLayer;
    public UnityEvent MouseClick;
    public UnityEvent MouseHover;
    public UnityEvent MouseExit;

    RaycastHit2D hit;
    bool hovering = false;

    void Awake()
    {
        interactableLayer = 1 << LayerMask.NameToLayer("Interactable");
    }

    void Start()
    {
        if (1 << this.gameObject.layer != interactableLayer)
            Debug.LogWarningFormat("'{0}' not on the Interactable layer", this.gameObject.name);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 1, interactableLayer);
        if (hit)
        {
            hovering = true;
            MouseHover.Invoke();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                MouseClick.Invoke();
        }
        else
        {
            if (hovering)
            {
                MouseExit.Invoke();
                hovering = false;
            }
        }
    }
}
