using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{

    GameObject[,] grid;
    private const float scale = 1f / BoardManager.TILE_SCALE;

    public Vector2 getNextTile(GameObject src, GameObject target) {

        float xr = src.transform.position.x * scale;
        float yr = src.transform.position.y * scale;

        Debug.Log(xr + ", " + yr);

        int x = (int)(xr);
        int y = (int)(yr);

        Vector2 dst = new Vector2(-1, -1);

        // Source is DST
        if (Vector3.Equals(src.transform.position, target.transform.position)) {
            return new Vector2(0, 0);
        }

        setTileDistance(x - 1, y, target, ref dst);
        setTileDistance(x + 1, y, target, ref dst);
        setTileDistance(x, y - 1, target, ref dst);
        setTileDistance(x, y + 1, target, ref dst);

        return new Vector2(dst.x - x, dst.y - y);
    }

    public GameObject getNearest(GameObject src, GameObject[] objects) {

        if (objects.Length == 0) {
            return null;
        }

        GameObject closest = objects[0];
        float dst = Vector3.Distance(src.transform.position, closest.transform.position);

        // Cycle Through all of the other objects comparing distance
        for (int i = 1; i < objects.Length; ++i) {
            GameObject target = objects[i];
            if (Vector3.Distance(src.transform.position, target.transform.position) < dst) {
                closest = target;
            }
        }

        return closest;
    }

    public void setTileDistance(int x, int y, GameObject target, ref Vector2 dir) {

        if (x >= 0 && x < BoardManager.GRID_SIZE &&
            y >= 0 && y < BoardManager.GRID_SIZE) {

            GameObject tile = grid[x, y];

            // Tile has not been set, there is probably a better way to do this
            if (dir.x == -1) {
                dir.Set(x, y);
                return;
            }

            GameObject best = grid[(int)dir.x, (int)dir.y];

            float currentTileDst = Vector3.Distance(tile.transform.position, target.transform.position);
            float bestTileDst = Vector3.Distance(best.transform.position, target.transform.position);

            if (currentTileDst < bestTileDst) {
                dir.Set(x, y);
            }

        }
    }

    public float getDistance(GameObject src, GameObject dst) {
        return Vector3.Distance(src.transform.position, src.transform.position);
    }

    public void setGrid(GameObject[,] grid) {
        this.grid = grid;
    }

}
