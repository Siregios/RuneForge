using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

// TODO : When generating a new map/puzzle, make sure to call ClearGrid()
public class GolemFetchManager : MonoBehaviour {

    public GolemController golem;
    public GameObject spawn;
    public GameObject cellObject;
    public int gridSize = 15;
    [HideInInspector]
    public Direction.DIRECTION entranceWall;
    public Vector2 startCell;

    public List<List<Cell>> grid = new List<List<Cell>>();

    void Awake()
    {
        InitializePuzzle();
    }

    void Update()
    {
        if (IsValidCell(golem.gridX, golem.gridY))
        {
            if (grid[golem.gridX][golem.gridY].orientation != Cell.CellOrientation.NONE)
            {
                Debug.LogFormat("{0} oriented cell at ({1}, {2})", grid[golem.gridX][golem.gridY].orientation, golem.gridX, golem.gridY);
            }
        }
    }

    void SpawnGolem(Vector2 pos)
    {
        golem.transform.position = pos;
        golem.movingDirection = Direction.OppositeDirection(entranceWall);
    }

    public void BeginTraversal()
    {
        golem.canMove = true;
    }

    void InitializePuzzle()
    {
        Camera.main.orthographicSize = (float)gridSize / 2;
        Camera.main.transform.position = new Vector3((float)gridSize / 2, (float)gridSize / 2, -10);

        entranceWall = PickEntranceWall();
        startCell = PickStartCell();
        spawn.transform.position = startCell;
        SpawnGolem(startCell);

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

    bool IsValidCell(int x, int y)
    {
        return (0 <= x && x < gridSize) && (0 <= y && y < gridSize);
    }

    Direction.DIRECTION PickEntranceWall()
    {
        return RandomEnum<Direction.DIRECTION>();
    }

    Vector2 PickStartCell()
    {
        int randomPos = Random.Range(2, gridSize - 3);
        switch (entranceWall)
        {
            case Direction.DIRECTION.LEFT:
                return new Vector2(0, randomPos);
            case Direction.DIRECTION.RIGHT:
                return new Vector2(gridSize - 1, randomPos);
            case Direction.DIRECTION.DOWN:
                return new Vector2(randomPos, 0);
            case Direction.DIRECTION.UP:
                return new Vector2(randomPos, gridSize - 1);
            default:
                return new Vector2(2, 0);
        }
    }

    void ClearGrid()
    {
        for (int c = 0; c < gridSize; c++)
        {
            for (int r = 0; r < gridSize; r++)
            {
                Destroy(grid[c][r].gameObject);
            }
            grid[c].Clear();
        }
        grid.Clear();
    }

    public T RandomEnum<T>()
    {
        var values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length));
    }
}