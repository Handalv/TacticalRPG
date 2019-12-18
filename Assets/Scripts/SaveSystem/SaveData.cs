using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // Main Data
    public int PlayerGold;
    public int MapSizeX;
    public int MapSizeZ;

    // Data for Cities
    public List<string> CityName;

    // Data for PlayerItems
    public List<string> Items;

    // Data for player's UnitList.
    public List<int> UnitHealth;
    public List<int> UnitDamage;
    public List<int> UnitSpeed;
    public List<string> UnitIconName;
    public List<bool> UnitIsOnBattleField;
    public List<int> UnitBattleIndex;
    public List<int> UnitStatus;

    // Data for Tiles of GlobalMap.
    public List<int> TileX;
    public List<int> TileZ;
    public List<string> TileType;

    // Data for coordinates on global map of player's Unit
    public int PlayerX;
    public int PlayerZ;

    //Data for MapObjects.
    public List<int> MapObjectX;
    public List<int> MapObjectZ;
    public List<string> MapObjectName;

    public SaveData(int mapSizeX, int mapSizeZ, MapObject playerUnit, Tile[,] tiles, UnitList unitList, List<MapObject> mapObjects, Inventory playerInventory)
    {
        #region List Initializating
        TileX = new List<int>();
        TileZ = new List<int>();
        MapObjectX = new List<int>();
        MapObjectZ = new List<int>();
        TileType = new List<string>();
        MapObjectName = new List<string>();

        Items = new List<string>();
        CityName = new List<string>();
        #endregion

        // Main
        MapSizeX = mapSizeX;
        MapSizeZ = mapSizeZ;

        // Player Unit on global map
        PlayerX = playerUnit.tileX;
        PlayerZ = playerUnit.tileZ;

        // Tiles
        foreach (Tile tile in tiles)
        {
            //Tiles
            TileX.Add(tile.tileX);
            TileZ.Add(tile.tileZ);
            this.TileType.Add(tile.type.name);
        }

        // MapObjects
        foreach (MapObject mapObject in mapObjects)
        {
            //Dont add player's Unit to others MapObjects
            if (mapObject == playerUnit)
                continue;
            //MapObject Basics
            MapObjectX.Add(mapObject.tileX);
            MapObjectZ.Add(mapObject.tileZ);
            MapObjectName.Add(mapObject.name);
           
            if (mapObject is City)
            {
                City city = (City)mapObject;
                CityName.Add(city.CityName);
            }
        }

        // player's UnitList
        SavePlayerUnits(unitList);

        // player's Inventory
        PlayerGold = playerInventory.Gold;

        foreach(Item item in playerInventory.Items)
        {
            Items.Add(item.name);
        }
    }

    public void SavePlayerUnits(UnitList unitList = null)
    {
        #region List Initializating
        UnitHealth = new List<int>();
        UnitDamage = new List<int>();
        UnitSpeed = new List<int>();
        UnitIconName = new List<string>();
        UnitIsOnBattleField = new List<bool>();
        UnitBattleIndex = new List<int>();
        UnitStatus = new List<int>();
        #endregion

        if (unitList == null)
        {
            unitList = UnitList.instance;
        }

        int index = 0;
        foreach (CreachureStats unit in unitList.units)
        {
            UnitHealth.Add(unit.MaxHealth);
            UnitDamage.Add(unit.Damage);
            UnitSpeed.Add(unit.Speed);
            UnitIconName.Add(unit.icon.name);
            UnitIsOnBattleField.Add(unitList.isOnBattleField[index]);
            UnitBattleIndex.Add(unitList.BattleFieldIndex[index]);
            UnitStatus.Add((int)unit.status);
            index++;
        }
    }
}