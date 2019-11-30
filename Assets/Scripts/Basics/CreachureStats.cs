using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreachureStats
{
    public Sprite icon;
    public int MaxHealth = 50;
    public int Health;
    public int Damage = 1;
    public int Speed = 5;
    public int VisionRange = 2;
    public int ActionPoints = 50;

    //UNDONE
    public int Level = 1;
    public int ExpForNextLevel = 20;
    public int Strength = 5;
    public int Agility = 5;
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
                Strength++;
                MaxHealth += 5;
                Health += 5;
                Intelegense++;
                int temp = ExpForNextLevel;
                ExpForNextLevel *= Level;
                ExpCurrent -= temp;
            }
        }
    }

    public int Cost
    {
        get
        {
            float value;
            //Scale by stats
            value = Level * 9;
            return (int)value;
        }
    }

    //public int MaxMana = 10;
    //public int Mana = 10;
    public Status status = 0;
    //


    public CreachureStats()
    {
        this.Health = MaxHealth;
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
