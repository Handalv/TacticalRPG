using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : TileMap
{
    public BattleMapGenerator mapGenerator;
    public UnitList unitList;
    //public UIGlobalMap UI;

    //// AWAKE INSTANCE
    //public static BattleMap instance;
    //void Awake()
    //{
    //    if (instance != null)
    //        Debug.Log("More than 1 BattleMap");
    //    instance = this;
    //}

    void Awake()
    {
        //unitList = FindObjectOfType<UnitList>();
        unitList = UnitList.instance;
    }

    void Start()
    {
        InitializeTiles();

        if (mapGenerator == null)
        {
            Debug.Log("MapGenerator in BattleMap is null by default");
            mapGenerator = GetComponent<BattleMapGenerator>();
        }

        InitializeTiles();
        mapGenerator.GenerateTiles();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        int offsetX = 3;
        int offsetZ = 7;
        //tileZ = battlefieldIndex % 6 + offsetZ;
        //tileX = battlefieldIndex / 6 + offsetX;
        for(int i=0; i< unitList.units.Count; i++ )
        {
            if (unitList.isOnBattleField[i])
            {
                GameObject spawned = GameObject.Instantiate(Resources.Load("BattlePlayerUnit")) as GameObject;
                BattleUnit battleUnit = spawned.GetComponent<BattleUnit>();

                int tileX = unitList.BattleFieldIndex[i] / 6 + offsetX;
                int tileZ = unitList.BattleFieldIndex[i] % 6 + offsetZ;

                battleUnit.tileX = tileX;
                battleUnit.tileZ = tileZ;
                battleUnit.unitStats = unitList.units[i];

                spawned.transform.position = ConvertTileCoordToWorld(battleUnit.tileX, battleUnit.tileZ);
                tiles[tileX, tileZ].mapObjects.Add(battleUnit);
            }
        }
    }
}
