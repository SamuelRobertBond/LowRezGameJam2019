using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{

    public int money = 600;
    public int income = 5;

    private int upgradeLevel = 1;

    private const int UPGRADE_ONE_COST = 100;
    private const int UPGRADE_TWO_COST = 500;

    protected CaptureEntity selectedCaptureEntity;
    public List<CaptureEntity> capturedEntities;

    private UnitManager unitManager;
    private BoardManager boardManager;

    // Start is called before the first frame update
    void Start()
    {
        this.capturedEntities = new List<CaptureEntity>();
        boardManager = FindObjectOfType<BoardManager>();
        unitManager = FindObjectOfType<UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets a captured Entity
    public void setCapturedEntity(CaptureEntity captureEntity)
    {
        captureEntity.owner = this;
        captureEntity.clearUnitQueue();
        this.capturedEntities.Add(captureEntity);

        if (selectedCaptureEntity) {
            selectedCaptureEntity = captureEntity;
        }

    }

    // Removes a captured Entity
    public void removeCaptureEntity(CaptureEntity captureEntity)
    {
        this.capturedEntities.Remove(captureEntity);
    }


    public void buyUnit(int unitLevel)
    {

        GameObject unitObj = unitManager.getUnit(upgradeLevel, unitLevel);
        GameUnitEntity unit = unitObj.GetComponent<GameUnitEntity>();

        int cost = unit.cost;
        int income = unit.income;

        if (this.money >= cost && selectedCaptureEntity.canQueueEntity()) {
            this.money -= cost;
            this.income += income;
            selectedCaptureEntity.queueUnit(new UnitSpawnInfo(unitObj, this));
        }

    }

    public void upgradeUnits()
    {

        if (upgradeLevel == 3) { return; } // No More Upgrades

        if (upgradeLevel == 1 && canBuy(UPGRADE_ONE_COST)) {
            this.money -= UPGRADE_ONE_COST;
            this.upgradeLevel += 1;
        } else if (canBuy(UPGRADE_TWO_COST)) {
            this.money -= UPGRADE_TWO_COST;
            this.upgradeLevel += 1;
        }

    }

    private bool canBuy(int cost) {
        return this.money >= cost;
    }

}
