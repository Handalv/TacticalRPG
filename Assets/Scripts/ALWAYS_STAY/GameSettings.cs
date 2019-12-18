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

    //List of every possible game tile
    public List<TileType> tileTypes;
    public List<Item> ItemList;

    // To Battle info
    public TileType BattleTileType;
    public List<UnitPreset> Enemies;
    public int EnemyMapIndex;

    //TEST playTime
    private float playTime = 0;

    //GameObject playerGoldText;

    //[SerializeField]
    //private int playerGold = 0;
    //public int PlayerGold
    //{
    //    get
    //    {
    //        return playerGold;
    //    }
    //    set
    //    {
    //        playerGold = value;
    //        playerGoldText.GetComponent<TextMeshProUGUI>().text = "" + PlayerGold;
    //    }
    //}

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

    void Update()
    {
        playTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(playTime + " secs on global map");
        }
    }

    void OnLevelWasLoaded(int level)
    {
        //1 - global map
        //2 - battle Map
        if (level == 1)
        {
            CurrentMap = GlobalMap.instance;
            //playerGoldText = UIGlobalMap.instance.playerGoldText;
            //PlayerGold = PlayerGold;
            playTime = 0;
        }
        if(level == 2)
        {
            CurrentMap = BattleMap.instance;
        }
    }

}
