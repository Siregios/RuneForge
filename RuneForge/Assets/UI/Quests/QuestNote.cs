using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class QuestNote : MonoBehaviour {
    public GameObject eventSystem;
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

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            GetComponent<Button>().image.color = Color.cyan;
        }

        else
        {
            GetComponent<Button>().image.color = Color.white;
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
