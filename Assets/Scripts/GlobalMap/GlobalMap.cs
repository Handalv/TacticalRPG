using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : TileMap
{
    public GlobalMapGenerator mapGenerator;
    public GameObject selectedUnit;

    public UIGlobalMap UI;

    //TEST PlayerPrefs variable
    public PlayerPrefs playerPrefs;

    // AWAKE INSTANCE
    public static GlobalMap instance;
    void Awake()
    {
        if (instance != null)
            Debug.Log("More than 1 GLobalMap");
        instance = this;

        //TEST PlayerPrefs setup
        if (playerPrefs == null)
        {
            Debug.Log("PlayerPrefs in MapGenerator is null by default");
            playerPrefs = FindObjectOfType<PlayerPrefs>();
        }
        //END TEST
    }

    new void Start()
    {
        base.Start();
        if (mapGenerator == null)
        {
            Debug.Log("MapGenerator in GlobalMap is null by default");
            mapGenerator = GetComponent<GlobalMapGenerator>();
        }

        GenerateMapVisual();
        //GeneratePathfindingGraph();

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

        tiles[x, z].mapObjects.Add(selectedUnit.GetComponent<MapObject>());

        selectedUnit.transform.position = ConvertTileCoordToWorld(x, z);
        RemoveGraphWarFog(x, z);
    }

    void RemoveGraphWarFog(int x, int z)
    {
        tiles[x, z].warFogEnabled = false;
        //Debug.Log("-----------------------" + graph[x, z].neighbours.Count);
        foreach (Node n in graph[x, z].neighbours)
        {
            //Debug.Log("x = " + n.x + "  z = " + n.z);
            tiles[n.x, n.z].warFogEnabled = false;
        }
    }
    void SetGraphWarFog(int x, int z)
    {
        if (globalWarfogEnabled)
        {
            tiles[x, z].warFogEnabled = true;

            //Debug.Log("x = " + x + "  z = " + z + " -----------------------" + graph[x, z].neighbours.Count);
            foreach (Node n in graph[x, z].neighbours)
            {
                //Debug.Log("x = " + n.x + "  z = " + n.z);
                tiles[n.x, n.z].warFogEnabled = true;
            }
        }
    }

    void SetGraphWarFogInRange(int x, int z, int Range = 0)
    {
        if (Range == 0)
            Range = playerPrefs.visionRange;

        //UNDONE Warfog Range
    }

    public void MoveUnit(int x, int z, GameObject unit = null)
    {
        if (unit == null)
            unit = selectedUnit;
        Vector3 unitTile = ConvertWorldCoordToTile(unit.transform.position);
        int oldX = (int)unitTile.x;
        int oldZ = (int)unitTile.z;
        tiles[oldX,oldZ].mapObjects.Remove(unit.GetComponent<MapObject>());
        //Debug.Log("x = " + X + "  z = " + Z + " -----------------------" + graph[X, Z].neighbours.Count);
        if (unit == selectedUnit)
        {
            SetGraphWarFog(oldX, oldZ);
            RemoveGraphWarFog(x, z);
        }
        else
        {
            unit.GetComponent<MapObject>().graphic.SetActive(!tiles[x, z].warFogEnabled);
        }
        unit.transform.position = ConvertTileCoordToWorld(x, z);

        if (tiles[x, z].mapObjects.Count > 0)
        {
            //TODO battle and others checks
            UI.OpenBattleMessage();
        }

        tiles[x, z].mapObjects.Add(unit.GetComponent<MapObject>());
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
