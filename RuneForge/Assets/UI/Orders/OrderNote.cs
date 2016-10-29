using UnityEngine;
using System.Collections;

public class OrderNote : MonoBehaviour {

    bool followingMouse = false;

    void Update()
    {
        if (followingMouse)
            this.transform.position = Input.mousePosition;
    }

    public void OnMouseDown()
    {
        followingMouse = true;
    }

    public void OnMouseRelease()
    {
        followingMouse = false;
    }
}
