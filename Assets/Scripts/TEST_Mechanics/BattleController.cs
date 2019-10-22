using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public List<CreachureStats> units = null;

    void Start()
    {
        UnitList playerUnits = FindObjectOfType<UnitList>();
        foreach(CreachureStats creachure in playerUnits.units)
        {
            units.Add(creachure);
        }
    }
}
