using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapGenerator : MonoBehaviour
{
    private TileType locationType;
    public BattleMap map;

    void Awake()
    {
        if (map == null)
        {
            Debug.Log("BattleMap in MapGenerator is null by default");
            map = GetComponent<BattleMap>();
        }
        locationType = FindObjectOfType<GlobalToBattleData>().tileType;
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < map.mapSizeX; x++)
            for (int z = 0; z < map.mapSizeZ; z++)
                map.tiles[x, z].SetTypeChanges(locationType);
    }

    public void GenerateMapObjects()
    {
        //Envoriment?
    }
}
