using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MapObject
{
    public float movementCD;

    public List<Node> currentPath = null;

    protected void Update()
    {
        if (currentPath != null)
        {
            int currNode = 0;
            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = TileMap.ConvertTileCoordToWorld(currentPath[currNode].x, currentPath[currNode].z) + new Vector3(0, 0.5f, 0);
                Vector3 end = TileMap.ConvertTileCoordToWorld(currentPath[currNode + 1].x, currentPath[currNode + 1].z) + new Vector3(0, 0.5f, 0);

                if (GlobalMap.instance.selectedUnit.GetComponent<Unit>() == this)
                    Debug.DrawLine(start, end, Color.cyan);
                else
                    Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }

        if (!GlobalMap.instance.GAMEPAUSED)
        {
            if (movementCD > 0)
            {
                movementCD -= Time.deltaTime;
            }
            else
            {
                if(currentPath != null)
                {
                    currentPath.RemoveAt(0);
                    GlobalMap.instance.MoveUnit(currentPath[0].x, currentPath[0].z, gameObject);
                    if (currentPath.Count == 1)
                    {
                        currentPath = null;
                        return;
                    }
                    movementCD = GlobalMap.instance.tiles[currentPath[1].x, currentPath[1].z].movementCost;
                }
            }
        }
    }

    public void SetDestanation(List<Node> destanation)
    {
        if (destanation != null)
        {
            currentPath = destanation;
            movementCD = GlobalMap.instance.tiles[currentPath[1].x, currentPath[1].z].movementCost;
        }
    }
}
