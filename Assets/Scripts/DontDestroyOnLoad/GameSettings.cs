using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    //void sortTiletipes()
    //{

    //    GrassTile, 0
    //    SandTile, 1
    //    WaterTile, 2
    //    RockTIle, 3
    //    SnowTile, 4
    //    RoadTile, 5
    //    SwampTile 6

    //}

    public TileType[] tileTypes;

    public static GameSettings instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
    }
}
