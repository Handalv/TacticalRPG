using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//void sortTiletipes()
//{

//    GrassTile, 0
//    SandTile, 1
//    WaterTile, 2
//    RockTIle, 3
//    SnowTile, 4
//    RoadTile, 5
//    SwampTile 6

//}

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
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

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
    public TileType[] tileTypes;

    UIGlobalMap globalMapUI;

    void Start()
    {
        PlayerGold = PlayerGold;
    }

}
