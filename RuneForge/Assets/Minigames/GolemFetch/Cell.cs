using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour {
    public Sprite bottomLeft, bottomRight, topRight, topLeft;
    public enum CellOrientation
    {
        NONE,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        TOP_RIGHT,
        TOP_LEFT
    }
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

    int orientationInt = 0;
    SpriteRenderer childSprite;
    Dictionary<CellOrientation, Sprite> orientationSprites = new Dictionary<CellOrientation, Sprite>();

    void Awake()
    {
        childSprite = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        orientationSprites.Add(CellOrientation.NONE, null);
        orientationSprites.Add(CellOrientation.BOTTOM_LEFT, bottomLeft);
        orientationSprites.Add(CellOrientation.BOTTOM_RIGHT, bottomRight);
        orientationSprites.Add(CellOrientation.TOP_LEFT, topLeft);
        orientationSprites.Add(CellOrientation.TOP_RIGHT, topRight);
    }

    void OnMouseDown()
    {
        orientationInt = (orientationInt + 1) % 5;
        childSprite.sprite = orientationSprites[orientation];
        //Debug.LogFormat("Clicked me: ({0}, {1}) - {2}", x, y, orientation.ToString());
    }
}
