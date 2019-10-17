//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
        //MoveAround();
    }

    void Update()
    {
        if (!GlobalMap.instance.GAMEPAUSED)
        {
            if (unit.currentPath == null)
            {
                MoveAround();
            }
        }
    }

    //return true if has tiles avalible to move
    public bool MoveAround()
    {
        List<Node> avliableTiles = new List<Node>();
        foreach(Node n in GlobalMap.instance.graph[unit.tileX, unit.tileZ].neighbours)
        {
            if (GlobalMap.instance.tiles[n.x, n.z].mapObjects.Count < 1)
                avliableTiles.Add(n);
        }     

        if (avliableTiles.Count == 0)
            return false;
        int r = Random.Range(0, avliableTiles.Count);
        List<Node> path = new List<Node>();
        path.Add(GlobalMap.instance.graph[unit.tileX, unit.tileZ]);
        path.Add(avliableTiles[r]);
        unit.SetDestanation(path);
        return true;
    }
}
