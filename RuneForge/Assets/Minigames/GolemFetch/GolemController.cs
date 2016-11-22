using UnityEngine;
using System.Collections;

public class GolemController : MonoBehaviour {
    [HideInInspector]
    public Direction.DIRECTION movingDirection;
    [HideInInspector]
    public bool canMove = false;
    public float speed = 5f;
    public int gridX
    {
        get { return Mathf.FloorToInt(actualPos.x); }
    }
    public int gridY
    {
        get { return Mathf.FloorToInt(actualPos.y); }
    }

    public Vector2 actualPos
    {
        get { return this.transform.position; }
    }

    private Vector2 previousCell, currentCell;

    void Update()
    {
        if (canMove)
        {
            Move();
        }
        UpdateCurrentCell();
    }

    void Move()
    {
        switch (movingDirection)
        {
            case Direction.DIRECTION.RIGHT:
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
                break;
            case Direction.DIRECTION.LEFT:
                this.transform.Translate(-speed * Time.deltaTime, 0, 0);
                break;
            case Direction.DIRECTION.UP:
                this.transform.Translate(0, speed * Time.deltaTime, 0);
                break;
            case Direction.DIRECTION.DOWN:
                this.transform.Translate(0, -speed * Time.deltaTime, 0);
                break;
        }
    }

    void UpdateCurrentCell()
    {
        Vector2 gridPos = new Vector2(gridX, gridY);
        if (PosCloseTo(actualPos, gridPos, (float)1/speed))
            currentCell = gridPos;
    }

    public bool EnteredNewCell()
    {
        if (currentCell != previousCell)
        {
            previousCell = currentCell;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SnapToGrid()
    {
        this.transform.position = new Vector2(gridX, gridY);
    }

    bool PosCloseTo(Vector2 actual, Vector2 desired, float range)
    {
        return (desired.x - range <= actual.x && actual.x <= desired.x + range) && (desired.y - range <= actual.y && actual.y <= desired.y + range);
    }
}
