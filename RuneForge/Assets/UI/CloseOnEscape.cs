using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CloseOnEscape : MonoBehaviour {
    public UnityEvent OnEscape;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape.Invoke();
        }
    }
}
