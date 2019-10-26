using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : TileMap
{
    private TileType locationType;
    public UnitList unitList;

    public static BattleMap instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }

        unitList = UnitList.instance;
        locationType = GameSettings.instance.BattleTileType;
    }

    void Start()
    {
        InitializeTiles();
        GenerateTiles();
        SpawnPlayer();
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
                tiles[x, z].SetTypeChanges(locationType);
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
