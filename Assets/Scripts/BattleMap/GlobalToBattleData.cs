using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalToBattleData : MonoBehaviour
{
    public TileType tileType;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
