using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MapObject
{
    public CreachureStats UnitStats;

    public int UnitHP
    {
        get
        {
            return UnitStats.Health;
        }
        set
        {
            UnitStats.Health = value;
            if (UnitStats.Health <= 0)
            {
                Die();
            }
        }
    }

    //[HideInInspector]
    public int CurrenActionpoints;
    public List<FightAction> Actions;

    void Die()
    {
        BattleController battleController = BattleController.instance;


        battleController.BattleOrder.Remove(this);
        if (battleController.CurrentBattleOrder.IndexOf(this) >= 0)
        {
            battleController.RemoveFromOrder(battleController.CurrentBattleOrder.IndexOf(this));
        }

        if (battleController.PlayerBattleList.IndexOf(this) >= 0)
        {
            battleController.PlayerBattleList.Remove(this);
            if (battleController.PlayerBattleList.Count == 0)
            {
                battleController.Defeat();
            }
        }
        if (battleController.EnemyBattleList.IndexOf(this) >= 0)
        {
            battleController.EnemyBattleList.Remove(this);
            if (battleController.EnemyBattleList.Count == 0)
            {
                battleController.Victory();
            }
        }

        BattleMap.instance.tiles[tileX, tileZ].mapObjects.Remove(this);

        if (BattleMap.instance.visibleObjects.Contains(this))
        {
            BattleMap.instance.visibleObjects.Remove(this);
        }

        Destroy(this.gameObject);
    }
}
