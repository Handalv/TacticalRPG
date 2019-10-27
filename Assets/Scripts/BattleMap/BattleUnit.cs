using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MapObject
{
    public CreachureStats UnitStats;

    public int CurrenActionpoints;
    public List<Node> CurrentPath = null;

    void Update()
    {
        if (CurrentPath != null)
        {
            int tileCost = BattleMap.instance.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost;
            if (tileCost <= CurrenActionpoints)
            {
                CurrenActionpoints -= tileCost;
                CurrentPath.RemoveAt(0);
                BattleMap.instance.MoveUnit(CurrentPath[0].x, CurrentPath[0].z, gameObject);
                if (CurrentPath.Count == 1)
                {
                    CurrentPath = null;
                    return;
                }
            }
        }
    }
}
