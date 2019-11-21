using System.Collections;
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
        Debug.Log("target received " + user.UnitStats.Damage + " damage and left" + target.UnitHP + "hp");
        //in AI and Player logic
        //user.UnitStats.ActionPoints -= Cost;
    }

    public override void CheckValidTargets(BattleUnit user, List<BattleUnit> skillTargets=null)
    {
        base.CheckValidTargets(user, skillTargets);
        Debug.Log("Attack valid target");
        if(skillTargets==null)
        {
            skillTargets = new List<BattleUnit>(Validtargets);
            //Validtargets = null;
        }
        Validtargets = new List<BattleUnit>();
        foreach (BattleUnit target in skillTargets)
        {
            if (BattleMap.instance.GeneratePathTo(target.tileX, target.tileZ, user.tileX, user.tileZ).Count - 1 <= Range)
            {
                Validtargets.Add(target);
            }
        }
    }
}
