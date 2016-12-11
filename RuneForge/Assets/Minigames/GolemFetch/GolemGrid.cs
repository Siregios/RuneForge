using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemGrid : MonoBehaviour
{
    public GameObject cellObject;
    public TextAsset map;

    public int size = 17;
    public List<List<Cell>> grid = new List<List<Cell>>();

    Cell.CellType StringToType(string symbol)
    {
        if (symbol == "O")
            return Cell.CellType.OBSTACLE;
        else
            return Cell.CellType.NONE;
    }

    List<List<string>> ReadMap()
    {
        List<List<string>> result = new List<List<string>>();

        string mapString = map.text;
        string[] rows = mapString.Split('\n');

        for (int i = 0; i < rows.Length; i++)
            result.Add(new List<string>());

        for (int y = rows.Length - 1; y >= 0; y--)
        {
            int x = 0;
            foreach (string cell in rows[y].Split(' '))
            {
                result[x].Add(cell);
                x++;
            }
        }

        return result;
    }

    public void CreateGrid(GolemFetchManager GolemGM)
    {
        List<List<string>> map = ReadMap();
        int c = 0;
        foreach (List<string> column in map)
        {
            grid.Add(new List<Cell>());
            int r = 0;
            foreach (string cellString in column)
            {
                Cell.CellType type = StringToType(cellString);

                GameObject newCellObject = (GameObject)Instantiate(cellObject, new Vector2(c, r), Quaternion.identity);
                Cell newCell = newCellObject.GetComponent<Cell>();
                newCell.Initialize(GolemGM);
                grid[c].Add(newCell);

                if (type == Cell.CellType.OBSTACLE)
                    newCell.SetType(Cell.CellType.OBSTACLE);
                else if (type == Cell.CellType.BOOK)
                    ; //TODO : Add location to bookList = possible locations for books;

                r++;
            }
            c++;
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
        return !((grid[x - 1][y].type == Cell.CellType.NONE && grid[x + 1][y].type == Cell.CellType.NONE) || (grid[x][y - 1].type == Cell.CellType.NONE && grid[x][y + 1].type == Cell.CellType.NONE));
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
    }
}
