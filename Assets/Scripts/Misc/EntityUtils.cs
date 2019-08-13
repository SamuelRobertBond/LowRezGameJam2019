using System.Collections.Generic;
using UnityEngine;

public class EntityUtils
{

    private static Dictionary<Direction, Direction> directions;
    private static Dictionary<Direction, Vector2> directionValues;

    public static void initDirections() {

        // Direction Enum Setup
        directions = new Dictionary<Direction, Direction>();
        directions.Add(Direction.RIGHT, Direction.UP);
        directions.Add(Direction.UP, Direction.LEFT);
        directions.Add(Direction.LEFT, Direction.DOWN);
        directions.Add(Direction.DOWN, Direction.RIGHT);

        // Direction Value Setup
        directionValues = new Dictionary<Direction, Vector2>();
        directionValues.Add(Direction.UP, new Vector2(0, 1));
        directionValues.Add(Direction.DOWN, new Vector2(0, -1));
        directionValues.Add(Direction.LEFT, new Vector2(-1, 0));
        directionValues.Add(Direction.RIGHT, new Vector2(1, 0));

    }

    // Returns Next Direction enum
    public static Direction getNextDirection(Direction current) {
        if (directions == null) {
            initDirections();
        }
        return directions[current];
    }

    // Returns movement values for direction
    public static Vector2 getDirectionVector(Direction current) {
        if (directions == null){
            initDirections();
        }
        return directionValues[current];
    }

}

public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
}