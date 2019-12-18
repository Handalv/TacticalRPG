using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MapObject
{
    [SerializeField]
    private int maxUnits = 2;
    private int unitsAmount = 0;

    [SerializeField]
    private float spawnCD = 150;
    private float currentSpawnCD;

    [SerializeField]
    private GameObject enemyPrefab;

    private void Start()
    {
        currentSpawnCD = spawnCD;
    }

    void Update()
    {
        if (unitsAmount < maxUnits)
        {
            currentSpawnCD -= Time.deltaTime;
            if (currentSpawnCD <= 0)
            {
                SpawnEnemy();
                currentSpawnCD = spawnCD;
            }
        }
    }

    void SpawnEnemy()
    {
        foreach (Node node in GlobalMap.instance.graph[tileX,tileZ].neighbours)
        {
            if (GlobalMap.instance.tiles[node.x, node.z].mapObjects.Count == 0)
            {
                GameObject newEnemy = GlobalMap.instance.AddMapObject("Enemy", node.x, node.z);
                unitsAmount++;
                return;
            }
        }
    }
}
