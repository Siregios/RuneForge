using UnityEngine;
using System;
using System.Collections;

public class Cell : MonoBehaviour {
    public enum CellOrientation
    {
        NONE,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        TOP_RIGHT,
        TOP_LEFT
    }
    int orientationInt = 0;

    public CellOrientation orientation
    {
        get { return (CellOrientation)orientationInt; }
    }
    public int x
    {
        get { return Mathf.FloorToInt(this.transform.position.x); }
    }
    public int y
    {
        get { return Mathf.FloorToInt(this.transform.position.y); }
    }

    void OnMouseDown()
    {
        orientationInt = (orientationInt + 1) % 5;
        Debug.LogFormat("Clicked me: ({0}, {1}) - {2}", x, y, orientation.ToString());
    }
}
