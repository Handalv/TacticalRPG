using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreachureStats
{
    public Sprite icon;
    public int MaxHealth = 10;
    public int Health = 10;
    public int Damage = 1;
    public int Speed = 1;
    public int VisionRange = 2;
    public int ActionPoints = 3;

    //UNDONE
    public int Level = 1;
    public int ExpForNextLevel = 20;
    public int Strange = 5;
    public int Intelegense = 5;
    public int ExpForKill = 5;

    int expCurrent = 0;
    public int ExpCurrent
    {
        get
        {
            return expCurrent;
        }
        set
        {
            expCurrent = value;
            if (expCurrent >= ExpForNextLevel)
            {
                Level++;
                Strange++;
                MaxHealth += 5;
                Health += 5;
                Intelegense++;
                int temp = ExpForNextLevel;
                ExpForNextLevel *= Level;
                ExpCurrent -= temp;
            }
        }
    }
    //public int MaxMana = 10;
    //public int Mana = 10;
    public Status status = 0;
    //


    public CreachureStats(int MaxHealth = 10, int Damage = 1, int Speed = 1)
    {
        this.MaxHealth = MaxHealth;
        this.Damage = Damage;
        this.Speed = Speed;
    }

    public CreachureStats(UnitPreset preset)
    {
        MaxHealth = Random.Range(preset.MinHeath, preset.MaxHeath+1);
        Health = MaxHealth;
        Damage = Random.Range(preset.MinDamage, preset.MaxDamage + 1);
        Speed = Random.Range(preset.MinSpeed, preset.MaxSpeed + 1);
        icon = preset.Icon;
    }
}

[System.Serializable]
public enum Status
{
    Fine,
    Stun
}
