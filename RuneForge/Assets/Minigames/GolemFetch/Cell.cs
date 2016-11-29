using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour {
    private GolemFetchManager golemFetchGM;

    public Sprite bottomLeft, bottomRight, topRight, topLeft;
    public Sprite spawn, book, obstacle, end;
    public enum CellOrientation
    {
        NONE,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        TOP_RIGHT,
        TOP_LEFT
    }
    public enum CellType
    {
        NONE,
        OBSTACLE,
        SPAWN,
        BOOK,
        END
    }
    public CellOrientation orientation
    {
        get { return (CellOrientation)orientationInt; }
    }
    public CellType type = CellType.NONE;
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
    Dictionary<CellType, Sprite> typeSprites = new Dictionary<CellType, Sprite>();

    void Awake()
    {
        childSprite = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();

        orientationSprites.Add(CellOrientation.NONE, null);
        orientationSprites.Add(CellOrientation.BOTTOM_LEFT, bottomLeft);
        orientationSprites.Add(CellOrientation.BOTTOM_RIGHT, bottomRight);
        orientationSprites.Add(CellOrientation.TOP_LEFT, topLeft);
        orientationSprites.Add(CellOrientation.TOP_RIGHT, topRight);

        typeSprites.Add(CellType.OBSTACLE, obstacle);
        typeSprites.Add(CellType.BOOK, book);
        typeSprites.Add(CellType.SPAWN, spawn);
        typeSprites.Add(CellType.END, end);
    }

    public void Initialize(GolemFetchManager golemFetchGM)
    {
        //SetType(type);
        this.golemFetchGM = golemFetchGM;
    }

    public void SetType(CellType type)
    {
        this.type = type;
        if (type != CellType.NONE)
            childSprite.sprite = typeSprites[type];
    }

    void OnMouseOver()
    {
        if (!golemFetchGM.traversing && type == CellType.NONE)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                orientationInt = (orientationInt + 1) % 5;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (orientationInt == 0)
                    orientationInt = 4;
                else
                    orientationInt = (orientationInt - 1) % 5;
            }
            childSprite.sprite = orientationSprites[orientation];
            //Debug.LogFormat("Clicked me: ({0}, {1}) - {2}", x, y, orientation.ToString());
        }
    }
}
