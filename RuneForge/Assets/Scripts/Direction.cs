public class Direction
{
    public enum DIRECTION
    {
        NONE,
        LEFT,
        RIGHT,
        DOWN,
        UP
    }

    public static DIRECTION OppositeDirection(DIRECTION direction)
    {
        switch (direction)
        {
            case DIRECTION.LEFT:
                return DIRECTION.RIGHT;
            case DIRECTION.RIGHT:
                return DIRECTION.LEFT;
            case DIRECTION.DOWN:
                return DIRECTION.UP;
            case DIRECTION.UP:
                return DIRECTION.DOWN;
            default:
                return DIRECTION.NONE;
        }
    }
}
