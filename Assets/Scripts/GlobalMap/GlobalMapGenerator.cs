using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapGenerator : MonoBehaviour
{
    public GlobalMap map;
    public FactionRelations factionRelations;

    TileType[] tileTypes;

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

    public void GenerateTIles()
    {
        //TODO just do it! (somehow)
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
