using System.Collections;
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

        InitializeTiles();
        mapGenerator.GenerateTiles();
        FactionRelations.instance.GenerateRelationships();

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
                //int playerX=0, playerZ=0;
                globalWarfogEnabled = true;
                //foreach (Tile tile in tiles)
                //{
                //    SetGraphWarFog(tile.tileX, tile.tileZ);
                //    if (tile.mapObjects.Count != 0)
                //        foreach (MapObject mo in tile.mapObjects)
                //        {
                //            // dont have some "playerunit" script to throw the "Player" tag, which is using only here
                //            // It's can be faction check (but they are unfinished too)
                //            if (mo.CompareTag("Player"))
                //            {
                //                playerX = tile.tileX;
                //                playerZ = tile.tileZ;
                //            }
                //        }
                //}
                //RemoveGraphWarFog(playerX, playerZ);
                ReShowWarFog();
            }
        }
        //END TEST
        if (Input.GetKeyDown(KeyCode.P))
        {
            GAMEPAUSED = !GAMEPAUSED;
            UI.SetPauseVision(GAMEPAUSED);
        }
    }
    
    void SpawnPlayer()
    {
        selectedUnit = GameObject.Instantiate(Resources.Load("PlayerUnit")) as GameObject;
        selectedUnit.name = "PlayerUnit";
        int x = Random.Range(0, mapSizeX);
        int z = Random.Range(0, mapSizeZ);

        MapObject mo = selectedUnit.GetComponent<MapObject>();

        mo.tileX = x;
        mo.tileZ = z;

        tiles[x, z].mapObjects.Add(mo);

        selectedUnit.transform.position = ConvertTileCoordToWorld(x, z);
        //TEST new warfog system
        //RemoveGraphWarFog(x, z);
        visibleObjects.Add(mo);
        ReShowWarFog();
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
        //TEST new warfog system
        //if (unit == playerUnit)
        //{
        //    SetGraphWarFog(unitMO.tileX, unitMO.tileZ);
        //    RemoveGraphWarFog(x, z);
        //}
        //else
        //{
        //    unitMO.graphic.SetActive(!tiles[x, z].warFogEnabled);
        //}

        unit.transform.position = ConvertTileCoordToWorld(x, z);

        if (tiles[x, z].mapObjects.Count > 0)
        {
            Engagement(x,z,unit, unitMO);
        }

        unitMO.tileX = x;
        unitMO.tileZ = z;
        tiles[x, z].mapObjects.Add(unitMO);

        if (visibleObjects.Contains(unitMO))
        {
            ReShowWarFog();
        }
        else
        {
            unitMO.graphic.SetActive(!tiles[x, z].warFogEnabled);
        }
    }

    void Engagement(int x, int z, GameObject unit, MapObject unitMO)
    {
        foreach (MapObject mapObject in tiles[x, z].mapObjects)
        {
            if (mapObject is City)
            {
                UI.OpenCityUI(mapObject as City);
                GAMEPAUSED = true;
                return;
            }
        }

        //TODO battle and others checks
        Debug.Log("Battle at " + x + " " + z);
        foreach (MapObject mapObject in tiles[x, z].mapObjects)
        {
            Debug.Log("На " + mapObject.gameObject.name + " напал " + unitMO.gameObject.name);
        }
        FindObjectOfType<GlobalToBattleData>().tileType = tiles[x, z].type;
        UI.ActiveBattleMessage(true);
    }

}