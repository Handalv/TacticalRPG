using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerControls : MonoBehaviour
{
    BattleMap map;
    BattleController battleController;
    public BattleUnit selectedUnit;

    public List<Node> CurrentPath = null;
    //public BattleUnit target = null;

    void Start()
    {
        map = BattleMap.instance;
        battleController = BattleController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPath != null)
        {
            int currNode = 0;
            while (currNode < CurrentPath.Count - 1)
            {
                Vector3 start = TileMap.ConvertTileCoordToWorld(CurrentPath[currNode].x, CurrentPath[currNode].z) + new Vector3(0, 0.5f, 0);
                Vector3 end = TileMap.ConvertTileCoordToWorld(CurrentPath[currNode + 1].x, CurrentPath[currNode + 1].z) + new Vector3(0, 0.5f, 0);
                Debug.DrawLine(start, end, Color.cyan);
                currNode++;
            }
        }
        if (battleController.isPlayerTurn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(transform.position, ray.direction);
            
            if (Input.GetMouseButtonDown(0))
            {
                //TODO this should be in battleController
                selectedUnit = battleController.CurrentBattleOrder[0];
                RaycastHit[] hits = Physics.RaycastAll(ray); ;
                Tile hitTile = null;
                foreach (RaycastHit hit in hits)
                {
                    hitTile = hit.collider.gameObject.GetComponent<Tile>();
                    if (hitTile != null)
                        break;
                }
                if (hitTile != null)
                {
                    if (CurrentPath != null && CurrentPath[CurrentPath.Count - 1] == map.graph[hitTile.tileX, hitTile.tileZ])
                    {
                        MoveUnit();
                    }
                    else
                    {
                        CurrentPath = map.GeneratePathTo(hitTile.tileX, hitTile.tileZ, selectedUnit.tileX, selectedUnit.tileZ);
                    }
                }
            }
        }
    }

    public void MoveUnit()
    {
        if (CurrentPath != null)
        {
            while (selectedUnit.CurrenActionpoints >= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost)
            {
                selectedUnit.CurrenActionpoints -= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost;
                map.MoveUnit(CurrentPath[0].x, CurrentPath[0].z, selectedUnit.gameObject);

                CurrentPath.RemoveAt(0);
                if (CurrentPath.Count == 0)
                {
                    CurrentPath = null;
                    return;
                }
            }
        }
        CurrentPath = null;
    }
}
