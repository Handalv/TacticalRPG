using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapGenerator : MonoBehaviour
{
    public GlobalMap map;

    TileType[] tileTypes;

    void Start()
    {
        if (map == null)
        {
            Debug.Log("GlobalMap in MapGenerator is null by default");
            map = GetComponent<GlobalMap>();
        }
    }

    public void GenerateTIles()
    {

    }

    public void GenerateMapObjects()
    {
        //TODO TEST
        int enemyAmount = 3;
        for(int i=0;i< enemyAmount; i++)
        {
            int X, Z;
            do
            {
                X = Random.Range(0, map.mapSizeX);
                Z = Random.Range(0, map.mapSizeZ);
            } while (map.tiles[X, Z].mapObjects.Count > 0);

            GlobalTile tile = map.tiles[X, Z];
            GameObject enemy = GameObject.Instantiate(Resources.Load("Enemy")) as GameObject;
            MapObject mapObject = enemy.GetComponent<MapObject>();

            enemy.transform.position = GlobalMap.ConvertTileCoordToWorld(X, Z);
            tile.mapObjects.Add(mapObject);
            if (tile.warFogEnabled)
                mapObject.graphic.SetActive(false);
        }
        //END TEST
    }
}
