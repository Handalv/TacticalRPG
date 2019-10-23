using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "Unit_Asset")]
public class UnitPreset : ScriptableObject
{
    public Sprite Icon;
    public int MinHeath = 10;
    public int MinDamage = 1;
    public int MinSpeed = 1;
    public int MaxHeath = 10;
    public int MaxDamage = 1;
    public int MaxSpeed = 1;
}