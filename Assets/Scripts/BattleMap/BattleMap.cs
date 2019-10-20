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

    new void Start()
    {
        base.Start();

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
        foreach (PlayerUnitStats unit in unitList.units)
        {
            if (unit.isOnBattlefield)
            {
                GameObject spawned = GameObject.Instantiate(Resources.Load("BattlePlayerUnit")) as GameObject;
                BattleUnit battleUnit = spawned.GetComponent<BattleUnit>();

                int tileX = unit.battlefieldIndex / 6 + offsetX;
                int tileZ = unit.battlefieldIndex % 6 + offsetZ;

                battleUnit.tileX = tileX;
                battleUnit.tileZ = tileZ;
                battleUnit.unitStats = unit;

                spawned.transform.position = ConvertTileCoordToWorld(battleUnit.tileX, battleUnit.tileZ);
                tiles[tileX, tileZ].mapObjects.Add(battleUnit);
            }
        }
    }
}
