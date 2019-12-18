using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : TileMap
{
    public UIGlobalMap UI;
    public MapObject BattleOpponent = null;

    private bool pause = false;
    public bool GAMEPAUSED
    {
        get
        {
            return pause;
        }
        set
        {
            pause = value;
            UI.SetPauseVision(GAMEPAUSED);
        }
    }

    // New feature
    public List<MapObject> mapObjects = null;

    [SerializeField]
    private int maxEnemyLairs = 1;
    private int currentEnemyLairs = 0;
    [SerializeField]
    private float minLairSpawnCD = 150;
    [SerializeField]
    private float maxLairSpawnCD = 210;
    private float currentLairSpawnCD;
    

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

        currentLairSpawnCD = Random.Range(minLairSpawnCD, maxLairSpawnCD);
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
        }
        if (currentEnemyLairs < maxEnemyLairs)
        {
            currentLairSpawnCD -= Time.deltaTime;
            if (currentLairSpawnCD <= 0)
            {
                int X, Z;
                do
                {
                    X = Random.Range(0, mapSizeX);
                    Z = Random.Range(0, mapSizeZ);
                } while (tiles[X, Z].mapObjects.Count > 0 || !tiles[X, Z].type.isWalkable);
                AddMapObject("EnemyLair", X, Z);
                currentEnemyLairs++;
                currentLairSpawnCD = Random.Range(minLairSpawnCD, maxLairSpawnCD);
            }
        }
    }

    public GameObject AddMapObject(string prefabName, int x, int z)
    {
        GameObject NewObject = GameObject.Instantiate(Resources.Load(prefabName)) as GameObject;
        MapObject mo = NewObject.GetComponent<MapObject>();
        NewObject.name = prefabName;

        mo.tileX = x;
        mo.tileZ = z;

        tiles[x, z].mapObjects.Add(mo);
        mapObjects.Add(mo);

        NewObject.transform.position = ConvertTileCoordToWorld(x, z);

        if (tiles[x, z].WarFogEnabled)
        {
            mo.SetGraphicActive(false);
        }

        return NewObject;
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

    //TEST
    public void GenerateMapObjects()
    {
        //generate 3 enemies
        int enemyAmount = 3;
        for (int i = 0; i < enemyAmount; i++)
        {
            int X, Z;
            do
            {
                X = Random.Range(0, mapSizeX);
                Z = Random.Range(0, mapSizeZ);
            } while (tiles[X, Z].mapObjects.Count > 0 || !tiles[X, Z].type.isWalkable);

            AddMapObject("Enemy", X, Z);
        }
        //generate 2 cities
        int cityAmount = 2;
        for (int i = 0; i < cityAmount; i++)
        {
            int X, Z;
            do
            {
                X = Random.Range(0, mapSizeX);
                Z = Random.Range(0, mapSizeZ);
            } while (tiles[X, Z].mapObjects.Count > 0 || !tiles[X, Z].type.isWalkable);

            AddMapObject("City", X, Z);
        }
    }
    //END
    void SpawnPlayer(int x,int z)
    {
        selectedUnit = AddMapObject("PlayerUnit", x, z);
        FindObjectOfType<PlayerControls>().playerUnit = selectedUnit.GetComponent<Unit>();
        selectedUnit.name = "PlayerUnit";

        MapObject mo = selectedUnit.GetComponent<MapObject>();

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
            //TODO Movement richabletiles logic
            return base.GeneratePathTo(x, z, startX, startZ);
        else
            return null;
    }
}