//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    MapObject mapObject;
    float movementCD;
    GlobalTile tileToMove;

    void Start()
    {
        mapObject = GetComponent<MapObject>();
        MoveAround();
    }

    void Update()
    {
        if (!GlobalMap.instance.GAMEPAUSED)
        {
            if (movementCD > 0)
            {
                movementCD -= Time.deltaTime;
            }
            else
            {
                if (tileToMove != null)
                {
                    GlobalMap.instance.MoveUnit(tileToMove.tileX, tileToMove.tileZ, gameObject);
                    tileToMove = null;
                    MoveAround();
                }
            }
        }
    }

    public bool MoveAround()
    {
        Vector3 tileCoord = GlobalMap.ConvertWorldCoordToTile(transform.position);
        int currX = (int)tileCoord.x;
        int currZ = (int)tileCoord.z;

        List<GlobalTile> avliableTiles = new List<GlobalTile>();
        foreach(Node n in GlobalMap.instance.graph[currX, currZ].neighbours)
        {
            if (GlobalMap.instance.tiles[n.x, n.z].mapObjects.Count < 1)
                avliableTiles.Add(GlobalMap.instance.tiles[n.x, n.z]);
        }     

        if (avliableTiles.Count == 0)
            return false;
        int r = Random.Range(0, avliableTiles.Count);
        movementCD = avliableTiles[r].movementCost;
        tileToMove = avliableTiles[r];
        return true;
    }
}
