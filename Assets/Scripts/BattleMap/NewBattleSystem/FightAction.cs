using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightAction
{
    public Sprite icon;
    public List<BattleUnit> Validtargets = null;
    public int Range;
    public int Cost;

    //public bool isPlayerSide;

    public abstract void Use(BattleUnit user, BattleUnit target);

    // return List of avaliable targets
    public abstract void CheckValidTargets(BattleUnit user, List<BattleUnit> skillTargets);
}

public class Attack : FightAction
{
    public int Damage;
    public override void Use(BattleUnit user, BattleUnit target)
    {
        if (!Validtargets.Contains(target))
        {
            Debug.Log("Invalid target");
            return;
        }

        target.UnitStats.Health -= user.UnitStats.Damage;
    }

    public override void CheckValidTargets(BattleUnit user, List<BattleUnit> skillTargets)
    {
        Validtargets = null;

        foreach(BattleUnit target in skillTargets)
        {
            if (BattleMap.instance.GeneratePathTo(target.tileX, target.tileZ, user.tileX, user.tileZ).Count <= Range)
                Validtargets.Add(target);
        }
    }
}