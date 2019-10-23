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

    public void Use(CreachureStats user, CreachureStats target)
    {
        // if (user.ActionPoints-Cost >= 0)

        float value = 0;

        for(int i = 0; i < Modifires.Count; i++)
        {
            value += (GetStatValue(user, Modifires[i]) * ModifyScale[i]);
        }

        if (isTargetEnemy)
            value *= -1;

        SetStatValue(target, TargetStat, (int)value);

        // user.ActionPoints -= Cost;

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

    void SetStatValue(CreachureStats unit, UnitStat stat, int value)
    {
        switch (stat)
        {
            case UnitStat.Health:
                unit.Health += value;
                break;
            case UnitStat.Damage:
                unit.Damage += value;
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