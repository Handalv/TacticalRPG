using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MapObject
{
    public CreachureStats UnitStats;

    public int CurrenActionpoints;

    //public List<BattleAction> Actions;
    public List<FightAction> Actions;
}
