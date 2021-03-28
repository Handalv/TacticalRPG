using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//    GrassTile, 0
//    SandTile, 1
//    WaterTile, 2
//    RockTIle, 3
//    SnowTile, 4
//    RoadTile, 5
//    SwampTile 6

public class GameSettings : MonoBehaviour
{
    // New feature
    public TileMap CurrentMap;
    public GameObject CurrentCanvas;

    //List of every possible game tile
    public List<TileType> tileTypes;
    public List<Item> ItemList;

    // To Battle info
    public TileType BattleTileType;
    public List<UnitPreset> Enemies;
    public int EnemyMapIndex;

    public static GameSettings instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Debug.Log("Deleting whole object with second GameSettings attached");
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void OnLevelWasLoaded(int level)
    {
        //1 - global map
        //2 - battle Map
        if (level == 1)
        {
            CurrentMap = GlobalMap.instance;
            CurrentCanvas = UIGlobalMap.instance.gameObject;
        }
        if(level == 2)
        {
            CurrentMap = BattleMap.instance;
            CurrentCanvas = UIBattleMap.instance.gameObject;
        }
    }

}
