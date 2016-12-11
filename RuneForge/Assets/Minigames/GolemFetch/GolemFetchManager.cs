﻿using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

// TODO : When generating a new map/puzzle, make sure to call ClearGrid()
public class GolemFetchManager : MonoBehaviour
{
    public Timer timer;
    public Score score;
    int scoreTemp;

    public GolemGrid Grid;
    public GolemController golem;
    public GameObject spawn;
    [HideInInspector]
    public bool traversing = false;
    [HideInInspector]
    public Direction.DIRECTION entranceWall;
    public Vector2 startCell;

    List<Vector2> bookList = new List<Vector2>();

    int numberOfBooks = 2;

    void Awake()
    {
        InitializePuzzle();
    }

    void Update()
    {
        Countdown();
        if (Grid.IsValidCell(golem.gridX, golem.gridY))
        {
            if (golem.EnteredNewCell())
            {
                Cell cell = Grid.grid[golem.gridX][golem.gridY];
                if (cell.type == Cell.CellType.NONE && cell.orientation != Cell.CellOrientation.NONE)
                {
                    Debug.LogFormat("{0} oriented cell at ({1}, {2})", Grid.grid[golem.gridX][golem.gridY].orientation, golem.gridX, golem.gridY);
                    golem.SnapToGrid();
                    TurnGolem(Grid.grid[golem.gridX][golem.gridY].orientation); //Not sure if this should just be "cell"
                }
                else if (cell.type == Cell.CellType.OBSTACLE)
                {
                    SpawnGolem();
                }
                else if (cell.type == Cell.CellType.BOOK)
                {
                    scoreTemp++;
                    Grid.grid[(int)cell.x][(int)cell.y].sprite.sprite = null;
                }
                else if (cell.type == Cell.CellType.END)
                {
                    score.addScore(scoreTemp * 100);
                    numberOfBooks += scoreTemp;
                    scoreTemp = 0;
                    Grid.Clear();
                    InitializePuzzle();
                }
            }
        }
        else
        {
            SpawnGolem();
        }
    }

    void Countdown()
    {
        if (timer.time <= 0)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }
    }

    List<Vector2> ValidBookLocations()
    {
        List<Vector2> result = new List<Vector2>();
        for (int c = 2; c < Grid.size - 2; c++)
        {
            for (int r = 2; r < Grid.size - 2; r++)
            {
                if (Grid.grid[c][r].type == Cell.CellType.NONE && !Grid.IsCornerCell(c, r))
                    result.Add(new Vector2(c, r));
            }
        }

        return result;
    }

    void SpawnGolem()
    {
        timer.stopTimer = false;
        traversing = false;
        golem.canMove = false;
        golem.transform.position = startCell;
        golem.SnapToGrid();
        golem.movingDirection = Direction.OppositeDirection(entranceWall);

        //Temporary
        scoreTemp = 0;
        foreach (Vector2 cell in bookList)
        {
            Grid.grid[(int)cell.x][(int)cell.y].SetType(Cell.CellType.BOOK);
        }
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
        traversing = true;
        timer.stopTimer = true;
    }

    void InitializePuzzle()
    {
        Camera.main.orthographicSize = (float)Grid.size / 2;
        Camera.main.transform.position = new Vector3((float)Grid.size / 2, (float)Grid.size / 2, -10);

        entranceWall = PickEntranceWall();
        startCell = PickStartCell();
        spawn.transform.position = startCell;

        Grid.CreateGrid(this);
        Grid.SetCellType((int)startCell.x, (int)startCell.y, Cell.CellType.SPAWN);

        //Temporary

        List<Vector2> possibleBookCells = ValidBookLocations();
        bookList.Clear();
        for (int i = 0; i < numberOfBooks; i++)
        {
            int randomCell = Random.Range(0, possibleBookCells.Count);
            bookList.Add(possibleBookCells[randomCell]);
            possibleBookCells.RemoveAt(randomCell);
        }
        Grid.grid[Grid.size - 1][0].SetType(Cell.CellType.END);

        SpawnGolem();
    }

    Direction.DIRECTION PickEntranceWall()
    {
        List<Direction.DIRECTION> directionList = new List<Direction.DIRECTION>() { Direction.DIRECTION.UP, Direction.DIRECTION.DOWN, Direction.DIRECTION.LEFT, Direction.DIRECTION.RIGHT };
        return directionList[Random.Range(0, directionList.Count)];
    }

    Vector2 PickStartCell()
    {
        int randomPos = Random.Range(2, Grid.size - 2);
        switch (entranceWall)
        {
            case Direction.DIRECTION.LEFT:
                return new Vector2(0, randomPos);
            case Direction.DIRECTION.RIGHT:
                return new Vector2(Grid.size - 1, randomPos);
            case Direction.DIRECTION.DOWN:
                return new Vector2(randomPos, 0);
            case Direction.DIRECTION.UP:
                return new Vector2(randomPos, Grid.size - 1);
            default:
                return new Vector2(2, 0);
        }
    }   
}