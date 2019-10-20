using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapGenerator : MonoBehaviour
{
    public GlobalMap map;
    public FactionRelations factionRelations;

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

    public void GenerateTiles()
    {
        //TODO just do it! (somehow)
        //for (int x = 0; x < map.mapSizeX; x++)
        //    for (int z = 0; z < map.mapSizeZ; z++)
        //    {
        //        if (z % 2 == 0)
        //            map.tiles[x, z].SetTypeChanges(GameSettings.instance.tileTypes[0]);
        //        else
        //            map.tiles[x, z].SetTypeChanges(GameSettings.instance.tileTypes[1]);
        //    }
        for (int x = 0; x < map.mapSizeX; x++)
            for (int z = 0; z < map.mapSizeZ; z++)
            {
                int r = Random.Range(0, GameSettings.instance.tileTypes.Length);
                map.tiles[x, z].SetTypeChanges(GameSettings.instance.tileTypes[r]);
            }
    }

    public void GenerateMapObjects()
    {
        //UNDONE generate 3 enemys
        int enemyAmount = 3;
        for (int i = 0; i < enemyAmount; i++)
        {
            int X, Z;
            do
            {
                X = Random.Range(0, map.mapSizeX);
                Z = Random.Range(0, map.mapSizeZ);
            } while (map.tiles[X, Z].mapObjects.Count > 0 || !map.tiles[X, Z].type.isWalkable);

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
        //END
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
