using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GolemFetchManager : MonoBehaviour {
    public enum Direction
    {
        LEFT,
        RIGHT,
        DOWN,
        UP
    }

    public GameObject cellObject;
    public int gridSize = 15;
    [HideInInspector]
    public Direction entranceWall;
    public Vector2 startCell;

    public List<List<Cell>> grid = new List<List<Cell>>();

    void Awake()
    {
        Camera.main.orthographicSize = (float)gridSize / 2;
        Camera.main.transform.position = new Vector3((float)gridSize / 2, (float)gridSize / 2, -10);

        entranceWall = PickEntranceWall();

        for (int c = 0; c < gridSize; c++)
        {
            grid.Add(new List<Cell>());
            for (int r = 0; r < gridSize; r++)
            {
                GameObject newCellObject = (GameObject)Instantiate(cellObject, new Vector2(c, r), Quaternion.identity);
                Cell newCell = newCellObject.GetComponent<Cell>();
                grid[c].Add(newCell);
            }
        }
    }

    Direction PickEntranceWall()
    {
        return RandomEnum<Direction>();
    }

    public T RandomEnum<T>()
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}