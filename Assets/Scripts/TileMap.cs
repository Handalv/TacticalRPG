using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

    public bool globalWarfogEnabled = true;

    public bool GAMEPAUSED = false;

    public int mapSizeX=10;
    public int mapSizeZ=10;

    public Tile[,] tiles;
    public Node[,] graph;

    protected void Start()
    {
        GeneratePathfindingGraph();
    }
    //void Update()
    //{
        
    //}

    protected void GeneratePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeZ];

        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
            {
                graph[x, z] = new Node();
                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
            {
                if (z % 2 == 0)
                {
                    if (x > 0)
                    {
                        graph[x, z].neighbours.Add(graph[x - 1, z]);
                        if (z > 0)
                            graph[x, z].neighbours.Add(graph[x - 1, z - 1]);
                        if (z < mapSizeZ - 1)
                            graph[x, z].neighbours.Add(graph[x - 1, z + 1]);
                    }
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x, z - 1]);
                    if (z < mapSizeZ - 1)
                        graph[x, z].neighbours.Add(graph[x, z + 1]);
                    if (x < mapSizeX - 1)
                        graph[x, z].neighbours.Add(graph[x + 1, z]);
                }
                else
                {
                    if (x < mapSizeX - 1)
                    {
                        graph[x, z].neighbours.Add(graph[x + 1, z]);
                        if (z > 0)
                            graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                        if (z < mapSizeZ - 1)
                            graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                    }
                    if (x > 0)
                        graph[x, z].neighbours.Add(graph[x - 1, z]);
                    if (z < mapSizeZ - 1)
                        graph[x, z].neighbours.Add(graph[x, z + 1]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x, z - 1]);
                }
            }
    }

    // If i decieding to keep instance, this dont need to be static,
    public static Vector3 ConvertTileCoordToWorld(int x, int z, int y = 0)
    {
        float worldZ = z * 1.55f;
        float worldX;
        worldX = x * 1.8f;
        if (z % 2 == 0)
            worldX -= 0.9f;

        return new Vector3(worldX, y, worldZ);
    }

    // the most buggy part
    // unexpected for me 5.4f / 1.8f == 2  but 5.401f /1.8f == 3
    // soo we add every world pos +0.01 before converting to get correct answer

    // For now i using that to get X and Z of objects what has a MapObject script
    // so i can just storage their X and Z in the script
    public static Vector3 ConvertWorldCoordToTile(Vector3 worldTile)
    {
        float tileZ = (worldTile.z + 0.01f) / 1.55f;
        float tileX = 0;
        if (worldTile.x % 1.8f == 0)
        {
            tileX = (worldTile.x + 0.01f) / 1.8f;
        }
        else
        {
            tileX = (worldTile.x + 0.9f + 0.01f) / 1.8f;
        }

        return new Vector3(tileX, worldTile.y, tileZ);
    }
}

[System.Serializable]
public enum TileType
{
    GrassTile,
    SandTile,
    WaterTile,
    RockTIle,
    SnowTile,
    RoadTile,
    SwampTile
}