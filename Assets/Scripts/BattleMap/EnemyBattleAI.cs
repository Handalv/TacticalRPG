using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{
    BattleController controller;
    BattleMap map;
// Public?
    public BattleUnit unit;
    public BattleUnit CurrentTarget;
    public List<Node> CurrentPath = null;

    void Start()
    {
        map = BattleMap.instance;
        controller = BattleController.instance;
        unit = gameObject.GetComponent<BattleUnit>();
    }

    void Update()
    {
        if(controller.CurrentOrder[0]==unit)
        {
          float minDistance = Math.Infinity;
            foreach(BattleUnit target in controller.PlayerUnits)
            {
                 float distance = transform.DostanceTo(target.transfom);
                 if(distance<minDistance)
                 {
                  minDistance = distance;
                  CurrentTarget = target;
                 }
            }
             CurrentPath = map.GeneratePathfindTo(CurrentTarget.tileX, CurrentTarget.tileZ, unit.tileX, unit.tileZ);
             CurrentPath.RemoveAt(CurrentPath.Count-1);
        }
        if(CurrentPath!=null)
        {
            while(unit.ActionPoints >= map.tiles[CurrentPath[0].x,CurrentPath[0].z].BattleMovementCost)
            {
             map.MoveUnit(CurrentPath[0].x,CurrentPath[0].z, unit);
             CurrentPath.RemoveAt(0);
             unit.ActionPoints - map.tiles[CurrentPath[0].x,CurrentPath[0].z].BattleMovementCost;
            }
        }
    }
}
