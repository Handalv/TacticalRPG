﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public GameObject selectedUnit;

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

    // the most buggy part, and removed for now
    // unexpected for me 5.4f / 1.8f == 2  but 5.401f /1.8f == 3
    // soo we add every world pos +0.01 before converting to get correct answer
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

    //TODO i need to deal with pathfinding
    // but the question, is the pathfinding correct for both map or it's should to be different?

    // this expects for battle map, but now needs to addaptive to global

    public void GeneratePathTo(int x, int z, GameObject unit = null)
    {
        if (unit == null)
            unit = selectedUnit;
        Unit unitComp = unit.GetComponent<Unit>();

        unitComp.currentPath = null;

        //if (UnitCanEnterTile(x, z) == false)
        //{
        //    return;
        //}


        List<Node> unvisited = new List<Node>();

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
        Node source = graph[unitComp.tileX, unitComp.tileZ];
        Node target = graph[x, z];
        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + tiles[v.x, v.z].movementCost;
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            //no way to the target
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Linq, hmm but i did not include linq
            //using System.Linq;
        currentPath.Reverse();
        unitComp.SetDestanation(currentPath);
    }

}

//[System.Serializable]
//public enum TileType
//{
//    GrassTile,
//    SandTile,
//    WaterTile,
//    RockTIle,
//    SnowTile,
//    RoadTile,
//    SwampTile
//}