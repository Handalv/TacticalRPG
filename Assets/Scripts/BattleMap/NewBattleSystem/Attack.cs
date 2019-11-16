﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : FightAction
{
    //public int Damage;
    public Attack()
    {
        isTargetsFriendly = false;
    }

    public override void Use(BattleUnit user, BattleUnit target)
    {
        //if (!Validtargets.Contains(target))
        //{
        //    Debug.Log("Invalid target");
        //    return;
        //}

        target.UnitHP -= user.UnitStats.Damage;
        //in AI and Player logic
        //user.UnitStats.ActionPoints -= Cost;
    }

    public override void CheckValidTargets(BattleUnit user, List<BattleUnit> skillTargets=null)
    {
        base.CheckValidTargets(user, skillTargets);
        Debug.Log("Attack valid target");
        if(skillTargets==null)
        {
            skillTargets = Validtargets;
            Validtargets = null;
        }
        foreach (BattleUnit target in skillTargets)
        {
            if (BattleMap.instance.GeneratePathTo(target.tileX, target.tileZ, user.tileX, user.tileZ).Count <= Range)
                Validtargets.Add(target);
        }
    }
}
