using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAI : MonoBehaviour
{
    BattleController controller;
    BattleMap map;
    public BattleUnit unit;
    public BattleUnit CurrentTarget = null;
    public List<Node> CurrentPath = null;

    void Start()
    {
        map = BattleMap.instance;
        controller = BattleController.instance;
        unit = gameObject.GetComponent<BattleUnit>();
    }

    public void StartTurn()
    {
        CurrentTarget = null;
        CurrentPath = null;

        FindClosestTarget();
        CurrentPath.RemoveAt(CurrentPath.Count - 1);

        MoveToTarget();
        Attack();
        BattleController.instance.EndTurn();
    }

    public void FindClosestTarget()
    {
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
    }

    public void MoveToTarget()
    {
        if (CurrentPath.Count == 0)
        {
            CurrentPath = null;
            return;
        }
        else
        {
            if(unit.CurrenActionpoints >= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost)
            {
                unit.CurrenActionpoints -= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost;
                map.MoveUnit(CurrentPath[0].x, CurrentPath[0].z, unit.gameObject);

                CurrentPath.RemoveAt(0);
                MoveToTarget();
            }
            else
                return;
        }
    }


    public void Attack()
    {
        if (CurrentPath == null)
        {
            if (unit.CurrenActionpoints >= unit.Actions[0].Cost)
            {
                unit.Actions[0].Use(unit, CurrentTarget);
                Attack();
            }
            else
                return;
        }
    }
}
