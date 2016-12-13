using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemGrid : MonoBehaviour
{
    public GolemFetchManager golemGM;
    public GameObject cellObject;

    [HideInInspector]
    public int size;
    public List<List<Cell>> grid = new List<List<Cell>>();

    List<Vector2> validBookLocs = new List<Vector2>();
    List<Vector2> currentBookLocs = new List<Vector2>();

    Cell.CellType StringToType(string symbol)
    {
        if (symbol == "O")
            return Cell.CellType.OBSTACLE;
        else if (symbol == "B")
            return Cell.CellType.BOOK;
        else
            return Cell.CellType.NONE;
    }

    List<List<Cell.CellType>> ReadMap(TextAsset map)
    {
        List<List<Cell.CellType>> result = new List<List<Cell.CellType>>();

        string mapString = map.text;
        string[] rows = mapString.Split('\n');

        for (int i = 0; i < rows.Length; i++)
            result.Add(new List<Cell.CellType>());

        for (int y = rows.Length - 1; y >= 0; y--)
        {
            int x = 0;
            foreach (string cell in rows[y].Split(' '))
            {
                result[x].Add(StringToType(cell));
                x++;
            }
        }

        return result;
    }

    public void CreateNewGrid(TextAsset textMap, int numOfBooks)
    {
        this.Clear();

        List<List<Cell.CellType>> map = ReadMap(textMap);
        size = map.Count;

        for (int c = 0; c < size; c++)
        {
            List<Cell.CellType> column = map[c];
            grid.Add(new List<Cell>());

            for (int r = 0; r < size; r++)
            {
                Cell.CellType type = column[r];

                GameObject newCellObject = (GameObject)Instantiate(cellObject, new Vector2(c, r), Quaternion.identity);
                Cell newCell = newCellObject.GetComponent<Cell>();
                newCell.Initialize(golemGM);
                grid[c].Add(newCell);

                if (type == Cell.CellType.OBSTACLE)
                    newCell.SetType(Cell.CellType.OBSTACLE);
                else if (type == Cell.CellType.BOOK)
                    validBookLocs.Add(new Vector2(c, r));
            }
        }

        currentBookLocs.Clear();
        for (int i = 0; i < numOfBooks; i++)
        {
            int randomCell = Random.Range(0, validBookLocs.Count);
            currentBookLocs.Add(validBookLocs[randomCell]);
            validBookLocs.RemoveAt(randomCell);
        }
    }

    public void SetBooks()
    {
        foreach (Vector2 cell in currentBookLocs)
        {
            grid[(int)cell.x][(int)cell.y].SetType(Cell.CellType.BOOK);
        }
    }

    public void SetCellType(int x, int y, Cell.CellType type)
    {
        grid[x][y].SetType(type);
    }

    public bool IsValidCell(int x, int y)
    {
        return (0 <= x && x < size) && (0 <= y && y < size);
    }

    public bool IsCornerCell(int x, int y)
    {
        return !((grid[x - 1][y].type == Cell.CellType.NONE && grid[x + 1][y].type == Cell.CellType.NONE) || 
            (grid[x][y - 1].type == Cell.CellType.NONE && grid[x][y + 1].type == Cell.CellType.NONE));
    }

    public void Clear()
    {
        for (int c = 0; c < size; c++)
        {
            for (int r = 0; r < size; r++)
            {
                Destroy(grid[c][r].gameObject);
            }
            grid[c].Clear();
        }
        grid.Clear();

        validBookLocs.Clear();
    }
}
