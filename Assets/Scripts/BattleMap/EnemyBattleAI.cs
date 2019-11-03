using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{
    BattleController controller;
    BattleMap map;
    public BattleUnit unit;
    public BattleUnit CurrentTarget;
    public List<Node> CurrentPath = null;

    void Start()
    {
        map = BattleMap.instance;
        controller = BattleController.instance;
        unit = gameObject.GetComponent<BattleUnit>();
    }

    public void StartTurn()
    {
        //TODO do whole logic here, instred of Update
        float minDistance = Mathf.Infinity;
        foreach (BattleUnit target in controller.PlayerBattleList)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                CurrentTarget = target;
            }
        }
        CurrentPath = map.GeneratePathTo(CurrentTarget.tileX, CurrentTarget.tileZ, unit.tileX, unit.tileZ);

        // Remove target unit to stop movement right in front of him
        CurrentPath.RemoveAt(CurrentPath.Count - 1);
        //TODO remove tiles counting on attack range
        if (CurrentPath.Count == 0)
        {
            Attack();
            CurrentPath = null;
        }
        if (CurrentPath != null)
        {
            while (unit.CurrenActionpoints >= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost)
            {
                unit.CurrenActionpoints -= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost;
                map.MoveUnit(CurrentPath[0].x, CurrentPath[0].z, unit.gameObject);

                CurrentPath.RemoveAt(0);
                if (CurrentPath.Count == 0)
                {
                    CurrentPath = null;
                    Attack();
                    return;
                }
            }
            BattleController.instance.EndTurn();
        }
    }

    public void Attack()
    {
        while (unit.Actions[0].Cost <= unit.CurrenActionpoints)
        {
            unit.Actions[0].Use(unit, CurrentTarget);
        }
        CurrentTarget = null;
        BattleController.instance.EndTurn();
    }
}
