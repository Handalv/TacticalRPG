using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Useless script, i can catch "tile state" somewhere else in undestructable scripts 
//GameSettings best choice for now.
public class GlobalToBattleData : MonoBehaviour
{
    public TileType tileType;

    public static GlobalToBattleData instance;
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
}
