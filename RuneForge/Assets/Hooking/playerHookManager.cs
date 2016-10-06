using UnityEngine;
using System.Collections;

public class playerHookScript : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
        }
	}
}
