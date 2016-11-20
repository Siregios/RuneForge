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
        get { return Mathf.RoundToInt(actualPos.x); }
    }
    public int gridY
    {
        get { return Mathf.RoundToInt(actualPos.y); }
    }

    public Vector2 actualPos
    {
        get { return this.transform.position; }
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
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
}
