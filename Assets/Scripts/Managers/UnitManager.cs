using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitManager : MonoBehaviour
{

    public GameObject[] units;

    public GameObject getUnit(int level, int unit) {
        return units[(level * unit) - 1];
    }
    
}
