﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : TileMap
{
    public GlobalMapGenerator mapGenerator;

    public UIGlobalMap UI;

    // AWAKE INSTANCE
    public static GlobalMap instance;
    void Awake()
    {
        if (instance != null)
            Debug.Log("More than 1 GLobalMap");
        instance = this;
    }

    new void Start()
    {
        //GeneratePathfindingGraph();  -  in base.Start()
        base.Start();
        if (mapGenerator == null)
        {
            Debug.Log("MapGenerator in GlobalMap is null by default");
            mapGenerator = GetComponent<GlobalMapGenerator>();
        }

        GenerateMapVisual();

        mapGenerator.GenerateRelationships();

        SpawnPlayer();
        mapGenerator.GenerateMapObjects();
    }

    void Update()
    {
        //TEST Remove warfog
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (globalWarfogEnabled)
            {
                foreach (Tile tile in tiles)
                    RemoveGraphWarFog(tile.tileX, tile.tileZ);
                globalWarfogEnabled = false;
            }
            else
            {
                int playerX=0, playerZ=0;
                globalWarfogEnabled = true;
                foreach (Tile tile in tiles)
                {
                    SetGraphWarFog(tile.tileX, tile.tileZ);
                    if (tile.mapObjects.Count != 0)
                        foreach (MapObject mo in tile.mapObjects)
                        {
                            //UNDONE valid player check
                            // dont have some "playerunit" script to throw the "Player" tag, which is using only here
                            // It's can be faction check (but they are unfinished too)
                            if (mo.CompareTag("Player"))
                            {
                                playerX = tile.tileX;
                                playerZ = tile.tileZ;
                            }
                        }
                }
                RemoveGraphWarFog(playerX, playerZ);
            }
        }
        //END TEST
        if (Input.GetKeyDown(KeyCode.P))
        {
            GAMEPAUSED = !GAMEPAUSED;
            UI.SetPauseVision(GAMEPAUSED);
        }
    }
    
    void GenerateMapVisual()
    {
        tiles = new Tile[mapSizeX, mapSizeZ];
        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
            {
                GameObject tile = GameObject.Instantiate(Resources.Load(TileType.GrassTile.ToString())) as GameObject;
                tile.transform.position = ConvertTileCoordToWorld(x, z);
                tile.transform.parent = gameObject.transform;

                Tile maptile = tile.GetComponent<Tile>();
                maptile.tileX = x;
                maptile.tileZ = z;
                maptile.map = this;
                maptile.warFogEnabled = true;
                tiles[x, z] = maptile;
            }
    }

    void SpawnPlayer()
    {
        selectedUnit = GameObject.Instantiate(Resources.Load("PlayerUnit")) as GameObject;

        int x = Random.Range(0, mapSizeX);
        int z = Random.Range(0, mapSizeZ);

        MapObject mo = selectedUnit.GetComponent<MapObject>();

        mo.tileX = x;
        mo.tileZ = z;

        tiles[x, z].mapObjects.Add(mo);

        selectedUnit.transform.position = ConvertTileCoordToWorld(x, z);
        RemoveGraphWarFog(x, z);
    }

    void RemoveGraphWarFog(int x, int z)
    {
        tiles[x, z].warFogEnabled = false;
        foreach (Node n in graph[x, z].neighbours)
        {
            tiles[n.x, n.z].warFogEnabled = false;
        }
    }
    void SetGraphWarFog(int x, int z)
    {
        if (globalWarfogEnabled)
        {
            tiles[x, z].warFogEnabled = true;
            foreach (Node n in graph[x, z].neighbours)
            {
                tiles[n.x, n.z].warFogEnabled = true;
            }
        }
    }

    void SetGraphWarFogInRange(int x, int z, int Range = 0)
    {
        if (Range == 0)
        {

        }
            //Range = playerPrefs.visionRange;

        //UNDONE Warfog Range
    }

    public void MoveUnit(int x, int z, GameObject unit = null)
    {
        if (unit == null)
            unit = selectedUnit;

        MapObject unitMO = unit.GetComponent<MapObject>();

        tiles[unitMO.tileX, unitMO.tileZ].mapObjects.Remove(unitMO);
        if (unit == selectedUnit)
        {
            SetGraphWarFog(unitMO.tileX, unitMO.tileZ);
            RemoveGraphWarFog(x, z);
        }
        else
        {
            unitMO.graphic.SetActive(!tiles[x, z].warFogEnabled);
        }

        unit.transform.position = ConvertTileCoordToWorld(x, z);

        if (tiles[x, z].mapObjects.Count > 0)
        {
            //TODO battle and others checks
            Debug.Log("Battle at " + x + " " + z);
            foreach (MapObject mapObject in tiles[x, z].mapObjects)
            {
                Debug.Log("На " + mapObject.gameObject.name + "напал");
                Debug.Log(unitMO.gameObject.name);
            }
            UI.OpenBattleMessage();
        }

        unitMO.tileX = x;
        unitMO.tileZ = z;
        tiles[x, z].mapObjects.Add(unitMO);
    }
}