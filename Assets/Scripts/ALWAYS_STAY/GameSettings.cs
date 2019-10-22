using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//    GrassTile, 0
//    SandTile, 1
//    WaterTile, 2
//    RockTIle, 3
//    SnowTile, 4
//    RoadTile, 5
//    SwampTile 6

public class GameSettings : MonoBehaviour
{

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

    //List of every possible game tile
    public List<TileType> tileTypes;

    UIGlobalMap globalMapUI;

    

    [SerializeField]
    private int playerGold = 0;
    public int PlayerGold
    {
        get
        {
            return playerGold;
        }
        set
        {
            playerGold = value;
            if (globalMapUI == null)
                globalMapUI = UIGlobalMap.instance;
            globalMapUI.playerGoldText.text = "" + PlayerGold;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        //1 - global map
        if (level == 1)
        {
            PlayerGold = PlayerGold;
        }
    }

}
