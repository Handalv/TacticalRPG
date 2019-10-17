using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapGenerator : MonoBehaviour
{
    public GlobalMap map;
    public FactionRelations factionRelations;

    public TileType[] tileTypes;

    void Awake()
    {
        if (map == null)
        {
            Debug.Log("GlobalMap in MapGenerator is null by default");
            map = GetComponent<GlobalMap>();
        }
        if (factionRelations == null)
        {
            Debug.Log("FactionRelations in MapGenerator is null by default");
            factionRelations = GetComponent<FactionRelations>();
        }
    }

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

    public void GenerateTIles()
    {
        //TODO just do it! (somehow)
        for (int x = 0; x < map.mapSizeX; x++)
            for (int z = 0; z < map.mapSizeZ; z++)
            {
                if (z % 2 == 0)
                    map.tiles[x, z].SetTypeChanges(tileTypes[0]);
                else
                    map.tiles[x, z].SetTypeChanges(tileTypes[1]);
            }
    }

    public void GenerateMapObjects()
    {
        //TEST generate 3 enemys
        int enemyAmount = 3;
        for (int i = 0; i < enemyAmount; i++)
        {
            int X, Z;
            do
            {
                X = Random.Range(0, map.mapSizeX);
                Z = Random.Range(0, map.mapSizeZ);
            } while (map.tiles[X, Z].mapObjects.Count > 0);

            Tile tile = map.tiles[X, Z];
            GameObject enemy = GameObject.Instantiate(Resources.Load("Enemy")) as GameObject;
            MapObject mapObject = enemy.GetComponent<MapObject>();

            mapObject.tileX = X;
            mapObject.tileZ = Z;

            enemy.transform.position = GlobalMap.ConvertTileCoordToWorld(X, Z);
            tile.mapObjects.Add(mapObject);
            if (tile.warFogEnabled)
                mapObject.graphic.SetActive(false);
        }
        //END TEST
    }

    public void GenerateRelationships()
    {
        //default factions for every game
        factionRelations.SetNewFaction("Bandits",FactionType.Bandits,-70);
        factionRelations.SetNewFaction("Deserters", FactionType.Deserters, -40);
        factionRelations.SetNewFaction("Neutral", FactionType.Neutral, 0);

        factionRelations.SetNewFaction("Player", FactionType.Neutral, 0);
    }
}
