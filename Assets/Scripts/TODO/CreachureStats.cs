using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreachureStats
{
    public Sprite icon;
    public Status status = 0;
    public int heath;
    public int damage;
    public int speed;
}

[System.Serializable]
public enum Status
{
    Fine,
    Stun
}
