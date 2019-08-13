using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureEntity : GameEntity
{

    public Direction direction;
    public Vector2 dirValue = new Vector2(1, 0);

    private BoardManager boardManager;
    private Queue<UnitSpawnInfo> unitQueue;

    protected override void Start()
    {
        base.Start();
        this.boardManager = FindObjectOfType<BoardManager>();
        this.unitQueue = new Queue<UnitSpawnInfo>();
        this.owner.setCapturedEntity(this);
    }

    protected override void Act()
    {}

    public void step() {

        if (unitQueue.Count > 0) {
            Debug.Log("Spawning Unit From Queue");
            UnitSpawnInfo unitInfo = unitQueue.Dequeue();
            this.boardManager.addEntity(this.transform.position, unitInfo.owner, this.dirValue, unitInfo.unit);
        }

    }

    public bool canQueueEntity() {
        return unitQueue.Count < 3;
    }

    public void queueUnit(UnitSpawnInfo unitInfo) {
        unitQueue.Enqueue(unitInfo);
    }

    public void clearUnitQueue() {
        unitQueue.Clear();
    }

    public void changeDirection() {
        this.direction = EntityUtils.getNextDirection(direction);
        this.dirValue = EntityUtils.getDirectionVector(direction);
    }

    public void setDirection(Direction direction) {
        this.direction = direction;
        this.dirValue = EntityUtils.getDirectionVector(direction);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameUnitEntity unit = collider.GetComponent<GameUnitEntity>();
        if (unit != null) {

            if (this.owner != unit.owner)
            {
                unit.owner.removeCaptureEntity(this);
                unit.owner.setCapturedEntity(this);
            }

            unit.dir = this.dirValue;
        }
    }

}
