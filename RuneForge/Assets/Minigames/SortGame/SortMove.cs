using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortMove : MonoBehaviour {

    public bool moveUp;
    public bool moveDown;
		
	void Update () {

        if (moveUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -3.5f, 0), Time.deltaTime * 12f);
            if (Mathf.Abs(transform.position.y + 3.5f) < 0.2f)
            {
                moveUp = false;
                transform.position = new Vector3(transform.position.x, -3.5f, 0);
                Color tmp = transform.parent.gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                transform.parent.gameObject.GetComponent<SpriteRenderer>().color = tmp;
                foreach (Transform child in transform.parent)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        if (moveDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -7f, 0), Time.deltaTime * 12f);
            if (Mathf.Abs(transform.position.y + 7) < 0.2f)
            {
                moveDown = false;
                transform.position = new Vector3(transform.position.x, -7f, 0);
            }
        }

    }
}
