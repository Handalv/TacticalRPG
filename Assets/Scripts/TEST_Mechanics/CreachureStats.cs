using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreachureStats
{
    public Sprite icon;
    public Status status = 0;
    public int heath=10;
    public int damage=1;
    public int speed=1;
}

[System.Serializable]
public enum Status
{
    Fine,
    Stun
}
