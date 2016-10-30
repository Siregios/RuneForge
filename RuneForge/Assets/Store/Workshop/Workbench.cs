using UnityEngine;
using System.Collections;

public class Workbench : MonoBehaviour {
    public WorkbenchUI workbenchPanel;

    void OnMouseDown()
    {
        workbenchPanel.gameObject.SetActive(true);
    }
}
