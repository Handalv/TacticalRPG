//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    MapObject mapObject;
    float currentMovement;

    void Start()
    {
        mapObject = GetComponent<MapObject>();
    }

    void Update()
    {
        //TEST move enemys on Key
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveAround();
        }
        //END TEST
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
        GlobalMap.instance.MoveUnit(avliableTiles[r].tileX, avliableTiles[r].tileZ, gameObject);
        return true;
    }
}
