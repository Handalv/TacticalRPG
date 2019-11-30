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

    public bool isUsingSkill = false;
    public FightAction UsingSkill;

    public static BattlePlayerControls instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("More than 1 instance " + this.GetType().ToString());
            Destroy(this);
        }
    }

    void Start()
    {
        map = BattleMap.instance;
        battleController = BattleController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Draw Path
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
        //Movement and Using skills
        if (battleController.isPlayerTurn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(transform.position, ray.direction);

            //Movement
            if (isUsingSkill == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
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
            //Using skill
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StopUsingSkill();
                }
                if (Input.GetMouseButtonDown(0))
                {
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
                        foreach(BattleUnit unit in hitTile.mapObjects)
                        {
                            if (UsingSkill.Validtargets.Contains(unit))
                            {
                                selectedUnit.CurrentActionpoints -= UsingSkill.Cost;
                                UsingSkill.Use(selectedUnit, unit);
                                StopUsingSkill();
                                break;
                            }
                        }
                    }
                    
                }
            }
        }
    }

    void StopUsingSkill()
    {
        foreach (BattleUnit unit in UsingSkill.Validtargets)
        {
            unit.HighlightDisable();
        }
        UsingSkill.Validtargets.Clear();
        UsingSkill = null;
        isUsingSkill = false;
    }

    public void MoveUnit()
    {
        if (CurrentPath != null)
        {
            while (selectedUnit.CurrentActionpoints >= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost)
            {
                selectedUnit.CurrentActionpoints -= map.tiles[CurrentPath[0].x, CurrentPath[0].z].BattleMovementCost;
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
