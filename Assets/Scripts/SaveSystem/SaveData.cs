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

    //Data for every MapObject.
    public List<int> MapObjectX;
    public List<int> MapObjectZ;
    public List<string> PrefabName;

    public SaveData(int mapSizeX, int mapSizeZ, MapObject playerUnit, Tile[,] tiles, UnitList unitList, int playerGold)
    {
        #region List Initializating
        TileX = new List<int>();
        TileZ = new List<int>();
        MapObjectX = new List<int>();
        MapObjectZ = new List<int>();
        TileType = new List<string>();
        PrefabName = new List<string>();
        UnitHealth = new List<int>();
        UnitDamage = new List<int>();
        UnitSpeed = new List<int>();
        UnitIconName = new List<string>();
        UnitIsOnBattleField = new List<bool>();
        UnitBattleIndex = new List<int>();
        UnitStatus = new List<int>();
        CityName = new List<string>();
        #endregion

        //Main
        PlayerGold = playerGold;
        MapSizeX = mapSizeX;
        MapSizeZ = mapSizeZ;

        //Player Unit on global map
        PlayerX = playerUnit.tileX;
        PlayerZ = playerUnit.tileZ;

        // Tiles and MapObjects
        foreach (Tile tile in tiles)
        {
            //Tiles
            TileX.Add(tile.tileX);
            TileZ.Add(tile.tileZ);
            this.TileType.Add(tile.type.name);


            //MapObjects
            foreach (MapObject mapObject in tile.mapObjects)
            {
                //Dont add player's Unit to others MapObjects
                if(mapObject == playerUnit)
                    continue;
                //MapObject Basics
                MapObjectX.Add(mapObject.tileX);
                MapObjectZ.Add(mapObject.tileZ);
                PrefabName.Add(mapObject.name);

                if(mapObject is City)
                {
                    City city = (City)mapObject;
                    CityName.Add(city.CityName);
                }
            }
        }

        // player's UnitList
        foreach(PlayerUnitStats unit in unitList.units)
        {
            UnitHealth.Add(unit.heath);
            UnitDamage.Add(unit.damage);
            UnitSpeed.Add(unit.speed);
            if(unit.icon!=null)
                UnitIconName.Add(unit.icon.ToString());
            else
            {
                UnitIconName.Add("Missing");
            }
            UnitIsOnBattleField.Add(unit.isOnBattlefield);
            UnitBattleIndex.Add(unit.battlefieldIndex);
            UnitStatus.Add((int)unit.status);
        }
    }
}