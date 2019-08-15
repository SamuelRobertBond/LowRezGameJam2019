using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    private float time = 0.0f;
    private float stepTime = .5f;

    public GameObject capturePoint;
    public GameObject floorTile;

    public const int GRID_SIZE = 13;
    public const float TILE_SCALE = .08f; // Tiles are 8 pixels, but for rendering ppu is 100 on asset

    private Transform boardHolder;
    private GameObject[,] boardTiles = new GameObject[GRID_SIZE, GRID_SIZE];
    private List<Vector3> gridPositions = new List<Vector3>();

    public AiControllerManager aiManager;

    private List<ControllerManager> controllers = new List<ControllerManager>();

    void InitControllers() {

        controllers.Add(FindObjectOfType<GameManager>().GetComponent<PlayerController>());
        foreach (AiController aiController in aiManager.getControllers()) {
            controllers.Add(aiController);
        }
    }

    void InitGrid() {

        this.boardHolder = new GameObject("Board").transform;
        gridPositions.Clear();

        // Generate The Grid Space
        for (int x = 0; x < GRID_SIZE; ++x) {

            for (int y = 0; y < GRID_SIZE; ++y) {

                float tileX = x * TILE_SCALE;
                float tileY = y * TILE_SCALE;

                GameObject tileInstance = Instantiate(floorTile, new Vector3(tileX, tileY, 0f), Quaternion.identity) as GameObject;
                this.gridPositions.Add(new Vector3(tileX, y * tileY, 0f));

                boardTiles[x, y] = tileInstance;
                tileInstance.transform.SetParent(this.boardHolder);
            }

        }

        // Generate Starting Flags

        // Corners
        createCaptureEntity(new Vector3(0, 0, 0), Direction.RIGHT, controllers[0]);
        createCaptureEntity(new Vector3(GRID_SIZE - 1, 0, 0), Direction.UP, controllers[1]);
        //createCaptureEntity(new Vector3(GRID_SIZE - 1, GRID_SIZE - 1, 0), Direction.LEFT, controllers[2]);
        //createCaptureEntity(new Vector3(0, GRID_SIZE - 1, 0), Direction.DOWN, controllers[3]);

        // Cross Sections
        createCaptureEntity(new Vector3(GRID_SIZE / 2, 0, 0), Direction.RIGHT);
        createCaptureEntity(new Vector3(GRID_SIZE - 1, GRID_SIZE / 2, 0), Direction.UP);
        createCaptureEntity(new Vector3(GRID_SIZE / 2, GRID_SIZE - 1, 0), Direction.LEFT);
        createCaptureEntity(new Vector3(0, GRID_SIZE / 2, 0), Direction.DOWN);

        // Center
        createCaptureEntity(new Vector3(GRID_SIZE/2, GRID_SIZE/2, 0), Direction.RIGHT);

        //PathfindingManager pf = GetComponent<PathfindingManager>();
        //pf.setGrid(boardTiles);
    }

    public void createCaptureEntity(Vector3 pos, Direction dir)
    {
        this.createCaptureEntity(pos, dir, null);
    }

    public void createCaptureEntity(Vector3 pos, Direction dir, ControllerManager owner) {

        GameObject captureEntityObj = Instantiate(capturePoint, pos * TILE_SCALE, Quaternion.identity) as GameObject;
        CaptureEntity entity = captureEntityObj.GetComponent<CaptureEntity>();

        entity.setDirection(dir);

        if (owner != null) {
            owner.setCapturedEntity(entity);          
        }

    }

    public void step()
    {

        // Move / Attack Check
        GameUnitEntity[] entities = FindObjectsOfType<GameUnitEntity>(); // This may need to be sorted

        Debug.Log("Entity Step");
        foreach (GameUnitEntity e in entities)
        {
            e.step(this);
        }

        aiManager.step();

        // Unit Death Check
        Debug.Log("Death Check");
        for (int i = 0; i < entities.Length; ++i)
        {
            if (entities[i].isDead()) {
                Debug.Log("isDead");
                Destroy(entities[i].gameObject);
            }
        }

        // Spawn Units / Income Check
        CaptureEntity[] captureEntities = FindObjectsOfType<CaptureEntity>(); // This may need to be sorted
        foreach (CaptureEntity e in captureEntities)
        {
            e.step();
        }

        // Change Score Check

        Debug.Log("Stepped");
    }

    public void addEntity(Vector2 pos, ControllerManager owner, Vector2 dir, GameObject entityType) {

        GameObject entity = Instantiate(entityType, pos, Quaternion.identity);

        // Assign Team Value to entity
        GameUnitEntity unit = entity.GetComponent<GameUnitEntity>();
        unit.owner = owner;
        unit.dir = dir;

    }

    public void SetupScene() {
        this.InitControllers();
        this.InitGrid();
    }

    public void Update()
    {

        time += Time.deltaTime;

        // TODO: Replace this with timer based interval
        if (time >= stepTime) {
            time -= stepTime;
            this.step(); // Core Game Loop
        }

    }
}
