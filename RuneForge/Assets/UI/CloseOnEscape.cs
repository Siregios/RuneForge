using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CloseOnEscape : MonoBehaviour {
    public UnityEvent OnEscape;
    bool noExit = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(0) && !noExit))
        {
            OnEscape.Invoke();
        }
    }

    public void setExitBool(bool set)
    {
        noExit = set;
    }
}
