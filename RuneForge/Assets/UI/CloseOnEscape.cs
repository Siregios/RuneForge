using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CloseOnEscape : MonoBehaviour {
    public UnityEvent OnEscape;
    bool exit = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(0) && exit))
        {
            OnEscape.Invoke();
        }
    }

    public void setExitBool(bool set)
    {
        exit = set;
    }
}
