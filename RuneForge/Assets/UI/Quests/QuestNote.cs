using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestNote : MonoBehaviour {

    public void OnMouseDown()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Image>().sprite == gameObject.transform.Find("QuestIcon").GetComponent<Image>().sprite)
        {
            Debug.Log("YOLO");
        }
    }
    //bool followingMouse = false;

    //void Update()
    //{
    //    if (followingMouse)
    //        this.transform.position = Input.mousePosition;
    //}

    //public void OnMouseDown()
    //{
    //    followingMouse = true;
    //}

    //public void OnMouseRelease()
    //{
    //    followingMouse = false;
    //}
}
