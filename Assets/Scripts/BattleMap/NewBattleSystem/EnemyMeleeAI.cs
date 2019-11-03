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
        CurrentPath.RemoveAt(CurrentPath.Count - 1);
    }


    public void Attack()
    {

    }
}
