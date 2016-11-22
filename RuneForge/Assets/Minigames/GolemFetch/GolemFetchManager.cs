﻿using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

// TODO : When generating a new map/puzzle, make sure to call ClearGrid()
public class GolemFetchManager : MonoBehaviour
{

    public GolemController golem;
    public GameObject spawn;
    public GameObject cellObject;
    public int gridSize = 17;
    [HideInInspector]
    public Direction.DIRECTION entranceWall;
    public Vector2 startCell;

    public List<List<Cell>> grid = new List<List<Cell>>();

    //Temporary
    List<Vector2> obstacleList = new List<Vector2>()
    {
        new Vector2(2, 2), new Vector2(3, 2), new Vector2(5, 2), new Vector2(8, 2), new Vector2(11, 2), new Vector2(13, 2), new Vector2(14, 2),
        new Vector2(2, 3), new Vector2(8, 3), new Vector2(14, 3),
        new Vector2(5, 4), new Vector2(7, 4), new Vector2(8, 4), new Vector2(9, 4), new Vector2(11, 4),
        new Vector2(2, 5), new Vector2(4, 5), new Vector2(12, 5), new Vector2(14, 5),
        new Vector2(6, 6), new Vector2(7, 6), new Vector2(9, 6), new Vector2(10, 6),
        new Vector2(4, 7), new Vector2(6, 7), new Vector2(10, 7), new Vector2(12, 7),
        new Vector2(2, 8), new Vector2(3, 8), new Vector2(4, 8), new Vector2(8, 8), new Vector2(12, 8), new Vector2(13, 8), new Vector2(14, 8),
        new Vector2(4, 9), new Vector2(6, 9), new Vector2(10, 9), new Vector2(12, 9),
        new Vector2(6, 10), new Vector2(7, 10), new Vector2(9, 10), new Vector2(10, 10),
        new Vector2(2, 11), new Vector2(4, 11), new Vector2(12, 11), new Vector2(14, 11),
        new Vector2(5, 12), new Vector2(7, 12), new Vector2(8, 12), new Vector2(9, 12), new Vector2(11, 12),
        new Vector2(2, 13), new Vector2(8, 13), new Vector2(14, 13),
        new Vector2(2, 14), new Vector2(3, 14), new Vector2(5, 14), new Vector2(8, 14), new Vector2(11, 14), new Vector2(13, 14), new Vector2(14, 14)
    };

    List<Vector2> bookList = new List<Vector2>()
    {
        new Vector2(6, 7), new Vector2(10, 11)
    };

    void Awake()
    {
        InitializePuzzle();
    }

    void Update()
    {

        if (IsValidCell(golem.gridX, golem.gridY))
        {
            if (golem.EnteredNewCell())
            {
                Cell cell = grid[golem.gridX][golem.gridY];
                if (cell.type == Cell.CellType.NONE && cell.orientation != Cell.CellOrientation.NONE)
                {
                    Debug.LogFormat("{0} oriented cell at ({1}, {2})", grid[golem.gridX][golem.gridY].orientation, golem.gridX, golem.gridY);
                    golem.SnapToGrid();
                    TurnGolem(grid[golem.gridX][golem.gridY].orientation);
                }
                else if (cell.type == Cell.CellType.OBSTACLE)
                    SpawnGolem();
                else if (cell.type == Cell.CellType.BOOK)
                    ; //TODO : Increment score
                else if (cell.type == Cell.CellType.END)
                {
                    ClearGrid();
                    InitializePuzzle();
                }
            }
        }
        else
        {
            SpawnGolem();
        }
    }

    void SpawnGolem()
    {
        golem.canMove = false;
        golem.transform.position = startCell;
        golem.SnapToGrid();
        golem.movingDirection = Direction.OppositeDirection(entranceWall);
    }

    void TurnGolem(Cell.CellOrientation cellOrientation)
    {
        if (golem.movingDirection == Direction.DIRECTION.RIGHT)
        {
            if (cellOrientation == Cell.CellOrientation.TOP_RIGHT)
                golem.movingDirection = Direction.DIRECTION.DOWN;
            else if (cellOrientation == Cell.CellOrientation.BOTTOM_RIGHT)
                golem.movingDirection = Direction.DIRECTION.UP;
            else
                SpawnGolem();
        }
        else if (golem.movingDirection == Direction.DIRECTION.LEFT)
        {
            if (cellOrientation == Cell.CellOrientation.TOP_LEFT)
                golem.movingDirection = Direction.DIRECTION.DOWN;
            else if (cellOrientation == Cell.CellOrientation.BOTTOM_LEFT)
                golem.movingDirection = Direction.DIRECTION.UP;
            else
                SpawnGolem();
        }
        else if (golem.movingDirection == Direction.DIRECTION.UP)
        {
            if (cellOrientation == Cell.CellOrientation.TOP_LEFT)
                golem.movingDirection = Direction.DIRECTION.RIGHT;
            else if (cellOrientation == Cell.CellOrientation.TOP_RIGHT)
                golem.movingDirection = Direction.DIRECTION.LEFT;
            else
                SpawnGolem();
        }
        else if (golem.movingDirection == Direction.DIRECTION.DOWN)
        {
            if (cellOrientation == Cell.CellOrientation.BOTTOM_LEFT)
                golem.movingDirection = Direction.DIRECTION.RIGHT;
            else if (cellOrientation == Cell.CellOrientation.BOTTOM_RIGHT)
                golem.movingDirection = Direction.DIRECTION.LEFT;
            else
                SpawnGolem();
        }
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
        SpawnGolem();

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
        grid[Mathf.FloorToInt(startCell.x)][Mathf.FloorToInt(startCell.y)].Initialize(Cell.CellType.SPAWN);

        //Temporary
        foreach (Vector2 cell in obstacleList)
        {
            int c = Mathf.FloorToInt(cell.x);
            int r = Mathf.FloorToInt(cell.y);
            //grid[c][r].type = Cell.CellType.OBSTACLE;
            grid[c][r].Initialize(Cell.CellType.OBSTACLE);
        }
        foreach (Vector2 cell in bookList)
        {
            grid[(int)cell.x][(int)cell.y].Initialize(Cell.CellType.BOOK);
        }
        grid[gridSize - 1][0].Initialize(Cell.CellType.END);
    }

    bool IsValidCell(int x, int y)
    {
        return (0 <= x && x < gridSize) && (0 <= y && y < gridSize);
    }

    Direction.DIRECTION PickEntranceWall()
    {
        List<Direction.DIRECTION> directionList = new List<Direction.DIRECTION>() { Direction.DIRECTION.UP, Direction.DIRECTION.DOWN, Direction.DIRECTION.LEFT, Direction.DIRECTION.RIGHT };
        return directionList[Random.Range(0, directionList.Count)];
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
}