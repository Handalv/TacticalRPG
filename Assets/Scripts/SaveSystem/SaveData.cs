using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{

}

[Serializable]
public class FullSaveData
{
    public int MapSizeX;
    public int MapSizeZ;

    public List<int> TileX;
    public List<int> TileZ;
    public List<string> TileType;

    public int PlayerX;
    public int PlayerZ;

    public List<int> MapObjectX;
    public List<int> MapObjectZ;
    public List<string> PrefabName;

    public FullSaveData(int mapSizeX, int mapSizeZ, MapObject playerUnit, Tile[,] tiles)
    {
        TileX = new List<int>();
        TileZ = new List<int>();
        MapObjectX = new List<int>();
        MapObjectZ = new List<int>();

        TileType = new List<string>();
        PrefabName = new List<string>();

        MapSizeX = mapSizeX;
        MapSizeZ = mapSizeZ;
        foreach (Tile tile in tiles)
        {
            TileX.Add(tile.tileX);
            TileZ.Add(tile.tileZ);
            this.TileType.Add(tile.type.name);
            foreach (MapObject mapObject in tile.mapObjects)
            {
                if(mapObject == playerUnit)
                {
                    PlayerX = playerUnit.tileX;
                    PlayerZ = playerUnit.tileZ;
                }
                MapObjectX.Add(mapObject.tileX);
                MapObjectZ.Add(mapObject.tileZ);
                PrefabName.Add(mapObject.name);
            }
        }
    }
}

[Serializable]
public class TilesData
{
    public List<int> X;
    public List<int> Z;

    public List<string> TileType;

    public TilesData(Tile[,] tiles)
    {
        foreach(Tile tile in tiles)
        {
            X.Add(tile.tileX);
            Z.Add(tile.tileZ);
            this.TileType.Add(tile.type.name);
        }
    }
}

[Serializable]
public class MapObjectsData
{
    public List<int> X;
    public List<int> Z;

    public List<string> PrefabName;

    public MapObjectsData(Tile[,] tiles)
    {
        foreach (Tile tile in tiles)
            foreach (MapObject mapObject in tile.mapObjects)
            {
                X.Add(mapObject.tileX);
                Z.Add(mapObject.tileZ);
                PrefabName.Add(mapObject.name);
            }
    }
}
