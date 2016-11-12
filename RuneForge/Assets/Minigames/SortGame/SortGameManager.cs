using UnityEngine;
using System.Collections;

public class SortGameManager : MonoBehaviour {

    public GameObject[] runes;
    bool grabbed = false;
    GameObject drag;

	void Start () {
	
	}
		
	void Update () {
	    if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit != null && hit.collider != null)
            {
                if (hit.collider.tag == "Red" || hit.collider.tag == "Yellow" || hit.collider.tag == "Blue" || hit.collider.tag == "Green")
                {
                    grabbed = true;
                    drag = hit.collider.gameObject;
                }

            }

            if (grabbed)
                Cursor.visible = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drag.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0f);
        }

            if (Input.GetMouseButtonUp(0))
        {
            Cursor.visible = true;
            grabbed = false;
        }
	}
}
