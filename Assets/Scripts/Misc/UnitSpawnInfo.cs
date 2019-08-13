using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnInfo
{

    public GameObject unit;
    public ControllerManager owner;

    public UnitSpawnInfo(GameObject unit, ControllerManager owner) {
        this.unit = unit;
        this.owner = owner;
    }

}
