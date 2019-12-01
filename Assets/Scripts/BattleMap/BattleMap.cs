using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : TileMap
{
    // cashe vars
    UnitList unitList;
    TileType locationType;
    BattleController battleController;

    public static BattleMap instance;
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
        unitList = UnitList.instance;
        locationType = GameSettings.instance.BattleTileType;
        battleController = BattleController.instance;
        battleController.map = this;

        GeneratePathfindingGraph();

        InitializeTiles();
        GenerateTiles();

        battleController.SpawnPlayerUnits();
        battleController.SpawnEnemyUnits();

        ReShowWarFog();

        battleController.InitializeOrder();
    }

    private void Update()
    {
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
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < mapSizeX; x++)
            for (int z = 0; z < mapSizeZ; z++)
                tiles[x, z].SetTypeChanges(locationType);
    }

    public void MoveUnit(int x, int z, GameObject unit = null)
    {
        if (unit == null)
            unit = battleController.CurrentBattleOrder[0].gameObject;

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
    }
    public override bool UnitCanEnterTile(int x, int z)
    {
        bool result = base.UnitCanEnterTile(x,z);

        if (tiles[x, z].mapObjects.Count > 0) 
            result = false;

        return result;
    }
}
