using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BattleAction", menuName = "BattleAction")]
public class BattleAction : ScriptableObject
{
    public Sprite icon;

    // true - Attack/Debuff Enemy
    // false - Heal/Buff Alie
    public bool isTargetEnemy;
    public UnitStat TargetStat;
    public List<UnitStat> Modifires;
    public List<float> ModifyScale;
    public int Range;
    public Animation Visual;
    public int Cost;
    public List<BattleAction> AdditionalActions;

    public void Use(BattleUnit user, BattleUnit target)
    {
        float value = 0;

        for(int i = 0; i < Modifires.Count; i++)
        {
            value += (GetStatValue(user.UnitStats, Modifires[i]) * ModifyScale[i]);
        }

        if (isTargetEnemy)
            value *= -1;

        SetStatValue(target.UnitStats, TargetStat, (int)value);
        user.CurrenActionpoints -= Cost;
    }

    int GetStatValue(CreachureStats unit, UnitStat stat)
    {
        switch(stat)
        {
            case UnitStat.Health:
                return unit.Health;
            case UnitStat.Damage:
                return unit.Damage;
            default:
                return 0;
        }
    }

    void SetStatValue(CreachureStats targetUnit, UnitStat targetStat, int value)
    {
        switch (targetStat)
        {
            case UnitStat.Health:
                targetUnit.Health += value;
                break;
            case UnitStat.Damage:
                targetUnit.Damage += value;
                break;
        }
    }

}


[System.Serializable]
public enum UnitStat
{
    //Speed,
    //Status,
    Health,
    Damage
    
}