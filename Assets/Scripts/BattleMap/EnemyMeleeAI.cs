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

        //TODO What the hell
        if(CurrentPath == null)
        {
            BattleController.instance.EndTurn();
            return;
        }

        CurrentPath.RemoveAt(CurrentPath.Count - 1);

        MoveToTarget();
        Attack();
        if (CurrentTarget == null && BattleController.instance.PlayerBattleList.Count != 0)
        {
            StartTurn();
        }
        else
        {
            CurrentPath = null;
            CurrentTarget = null;
            BattleController.instance.EndTurn();
        }
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
            if(unit.CurrentActionpoints >= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost)
            {
                unit.CurrentActionpoints -= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost;
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
            if (unit.CurrentActionpoints >= unit.Actions[0].Cost)
            {
                Debug.Log("I am attacking with AP = " + unit.CurrentActionpoints);
                unit.CurrentActionpoints -= unit.Actions[0].Cost;
                unit.Actions[0].Use(unit, CurrentTarget);
                if (CurrentTarget.UnitHP <= 0)
                {
                    CurrentTarget = null;
                    return;
                }
                Attack();
            }
            else
                return;
        }
    }
}
