using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{

}

[Serializable]
public class TilesData
{
    public List<int> X;
    public List<int> Z;

    public List<string> TileType;

    public TilesData(List<Tile> tiles)
    {
        foreach(Tile tile in tiles)
        {
            X.Add(tile.tileX);
            Z.Add(tile.tileZ);
            this.TileType.Add(tile.type.ToString());
        }
    }
}

[Serializable]
public class MapObjectsData
{
    public List<int> X;
    public List<int> Z;

    public List<string> PrefabName;

    public MapObjectsData(List<Tile> tiles)
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
