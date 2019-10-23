using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerUnitStats : CreachureStats
{
    public bool isOnBattlefield = false;
    public int battlefieldIndex;

    public PlayerUnitStats(UnitPreset unitPreset) : base(unitPreset)
    {
        //UNDONE костыль
    }

    public PlayerUnitStats(int MaxHealth = 10, int Damage = 1, int Speed = 1) : base(MaxHealth, Damage, Speed)
    {
        //UNDONE костыль
    }
}
