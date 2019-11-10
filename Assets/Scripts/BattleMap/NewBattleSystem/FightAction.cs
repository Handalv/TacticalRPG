using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public /*abstract*/ class FightAction : ScriptableObject
{
    public Sprite icon;
    public List<BattleUnit> Validtargets = null;
    public int Range;
    public int Cost;

    public bool isTargetsFriendly = false;

    public virtual void Use(BattleUnit user, BattleUnit target) { }

    // return List of avaliable targets
    public virtual void CheckValidTargets(BattleUnit user, List<BattleUnit> skillTargets) { }
}

[CreateAssetMenu(fileName = "New Attack", menuName = "BattleActions/Attack")]
public class Attack : FightAction
{
    //public int Damage;
    public Attack()
    {
        isTargetsFriendly = false;
    }
  
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