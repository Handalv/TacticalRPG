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
    public virtual void CheckValidTargets(BattleUnit user, List<BattleUnit> skillTargets = null)
    {
        Debug.Log("Base valid target");
        Validtargets = null;
        if (skillTargets == null)
        {
            bool isPlayerUser = BattleController.instance.PlayerBattleList.Contains(user);
            if (isPlayerUser)
            {
                if (isTargetsFriendly)
                {
                    Validtargets = new List<BattleUnit>(BattleController.instance.PlayerBattleList);
                }
                else
                {
                    Validtargets = new List<BattleUnit>(BattleController.instance.EnemyBattleList);
                }
            }
            else
            {
                if (isTargetsFriendly)
                {
                    Validtargets = new List<BattleUnit>(BattleController.instance.EnemyBattleList);
                }
                else
                {
                    Validtargets = new List<BattleUnit>(BattleController.instance.PlayerBattleList);
                }
            }
        }
    }
}