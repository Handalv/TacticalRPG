using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : TileMap
{
    public UIGlobalMap UI;
    public MapObject BattleOpponent = null;

    // New feature
    public List<MapObject> mapObjects = null;

    // AWAKE INSTANCE
    public static GlobalMap instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }
    }

    void Start()
    {
        if (SaveManager.instance.isLoadGame)
        {
            //SaveManager.instance.LoadGame();
            SaveData save = SaveManager.instance.save;

            mapSizeX = save.MapSizeX;
            mapSizeZ = save.MapSizeZ;

            Debug.Log(mapSizeX + " " + mapSizeZ);

            GeneratePathfindingGraph();
            InitializeTiles();

            LoadTiles(save);
            LoadMapObjects(save);
        }
        else
        {
            GeneratePathfindingGraph();
            InitializeTiles();

            FactionRelations.instance.GenerateRelationships();

            GenerateTiles();
            int x = Random.Range(0, mapSizeX);
            int z = Random.Range(0, mapSizeZ);
            SpawnPlayer(x, z);
            GenerateMapObjects();
        }
    }

    void LoadTiles(SaveData save)
    {
        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
            {
                TileType t = GameSettings.instance.tileTypes.Find(type => type.name == save.TileType[(x*mapSizeX) + z]);
                tiles[x, z].SetTypeChanges(t);
            }
    }

    void LoadMapObjects(SaveData save)
    {
        SpawnPlayer(save.PlayerX, save.PlayerZ);

        int mapObjectIndex = 0;
        int cityIndex = 0;
        foreach(string objName in save.MapObjectName)
        {
            Tile tile = tiles[save.MapObjectX[mapObjectIndex], save.MapObjectZ[mapObjectIndex]];

            GameObject go = GameObject.Instantiate(Resources.Load(objName)) as GameObject;
            go.name = objName;

            MapObject mapObject = go.GetComponent<MapObject>();
            mapObject.tileX = save.MapObjectX[mapObjectIndex];
            mapObject.tileZ = save.MapObjectZ[mapObjectIndex];
            
            if(mapObject is City)
            {
                City city = (City)mapObject;
                city.CityName = save.CityName[cityIndex];

                cityIndex++;
            }

            go.transform.position = GlobalMap.ConvertTileCoordToWorld(mapObject.tileX, mapObject.tileZ);

            tile.mapObjects.Add(mapObject);
            mapObjects.Add(mapObject);

            mapObjectIndex++;
        }

        ReShowWarFog();
    }

    void Update()
    {
        //TEST Remove warfog
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (globalWarfogEnabled)
            {
                foreach (Tile tile in tiles)
                    tile.WarFogEnabled = false;
                globalWarfogEnabled = false;
            }
            else
            {
                globalWarfogEnabled = true;
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
        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
            {
                int r = Random.Range(0, GameSettings.instance.tileTypes.Count);
                tiles[x, z].SetTypeChanges(GameSettings.instance.tileTypes[r]);
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
                X = Random.Range(0, mapSizeX);
                Z = Random.Range(0, mapSizeZ);
            } while (tiles[X, Z].mapObjects.Count > 0 || !tiles[X, Z].type.isWalkable);

            Tile tile = tiles[X, Z];
            GameObject go = GameObject.Instantiate(Resources.Load("Enemy")) as GameObject;
            go.name = "Enemy";
            MapObject mapObject = go.GetComponent<MapObject>();

            mapObject.tileX = X;
            mapObject.tileZ = Z;

            go.transform.position = GlobalMap.ConvertTileCoordToWorld(X, Z);
            tile.mapObjects.Add(mapObject);
            mapObjects.Add(mapObject);
            if (tile.WarFogEnabled)
                mapObject.SetGraphicActive(false);
        }
        //generate 2 citys
        int cityAmount = 2;
        for (int i = 0; i < cityAmount; i++)
        {
            int X, Z;
            do
            {
                X = Random.Range(0, mapSizeX);
                Z = Random.Range(0, mapSizeZ);
            } while (tiles[X, Z].mapObjects.Count > 0 || !tiles[X, Z].type.isWalkable);

            Tile tile = tiles[X, Z];
            GameObject go = GameObject.Instantiate(Resources.Load("City")) as GameObject;
            go.name = "City";
            MapObject mapObject = go.GetComponent<MapObject>();

            mapObject.tileX = X;
            mapObject.tileZ = Z;

            go.transform.position = GlobalMap.ConvertTileCoordToWorld(X, Z);
            tile.mapObjects.Add(mapObject);
            mapObjects.Add(mapObject);
            if (tile.WarFogEnabled)
                mapObject.SetGraphicActive(false);
        }
        //END
    }

    void SpawnPlayer(int x,int z)
    {
        selectedUnit = GameObject.Instantiate(Resources.Load("PlayerUnit")) as GameObject;
        FindObjectOfType<PlayerControls>().playerUnit = selectedUnit.GetComponent<Unit>();
        selectedUnit.name = "PlayerUnit";

        MapObject mo = selectedUnit.GetComponent<MapObject>();

        mo.tileX = x;
        mo.tileZ = z;

        tiles[x, z].mapObjects.Add(mo);
        mapObjects.Add(mo);

        selectedUnit.transform.position = ConvertTileCoordToWorld(x, z);
        //TEST new warfog system
        //RemoveGraphWarFog(x, z);
        visibleObjects.Add(mo);
        ReShowWarFog();
    }

    public void MoveUnit(int x, int z, GameObject unit = null)
    {
        if (unit == null)
            unit = selectedUnit;

        MapObject unitMO = unit.GetComponent<MapObject>();

        unit.transform.position = ConvertTileCoordToWorld(x, z);
        tiles[unitMO.tileX, unitMO.tileZ].mapObjects.Remove(unitMO);
        unitMO.tileX = x;
        unitMO.tileZ = z;

        tiles[x, z].mapObjects.Add(unitMO);

        if (visibleObjects.Contains(unitMO))
        {
            ReShowWarFog();
        }
        else
        {
            unitMO.SetGraphicActive(!tiles[x, z].WarFogEnabled);
        }
        if (tiles[x, z].mapObjects.Count > 1)
        {
            Engagement(x, z, unit, unitMO);
        }
    }

    void Engagement(int x, int z, GameObject unit, MapObject unitMO)
    {
        bool isPlayerEngagement = false;

        foreach (MapObject mapObject in tiles[x, z].mapObjects)
        {
            if (mapObject == selectedUnit.GetComponent<MapObject>())
            {
                isPlayerEngagement = true;
            }
        }

        Debug.Log("Engagement at " + x + " " + z + " is player - " + isPlayerEngagement.ToString());

        foreach (MapObject mapObject in tiles[x, z].mapObjects)
        {
            if (mapObject is City && isPlayerEngagement)
            {
                UI.OpenCityUI(mapObject as City);
                GAMEPAUSED = true;
                return;
            }
        }

        //TODO battle and others checks
        foreach (MapObject mapObject in tiles[x, z].mapObjects)
        {
            if (isPlayerEngagement)
            {
                EnemyUnitList enemyUnitList = mapObject.gameObject.GetComponent<EnemyUnitList>();
                if (enemyUnitList!=null)
                {
                    //-1 becoz element 0 is player unit wich will be thrown while save from list
                    GameSettings.instance.EnemyMapIndex = mapObjects.IndexOf(mapObject)-1;
                    Debug.Log("enenmy index " + GameSettings.instance.EnemyMapIndex);


                    GameSettings.instance.BattleTileType = tiles[x, z].type;
                    GameSettings.instance.Enemies = enemyUnitList.Enemies;
                    BattleOpponent = mapObject;
                    GAMEPAUSED = true;
                    UI.ActiveBattleMessage(true);
                    return;
                }
            }
        }
    }

    public new List<Node> GeneratePathTo(int x, int z, int startX, int startZ)
    {
        if (UnitCanEnterTile(x, z))
            return base.GeneratePathTo(x, z, startX, startZ);
        else
            return null;
    }
}