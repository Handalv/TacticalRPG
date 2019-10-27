using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerControls : MonoBehaviour
{
    BattleMap map;
    BattleController battleController;

    void Start()
    {
        map = BattleMap.instance;
        battleController = BattleController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleController.instance.isPlayerTurn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(transform.position, ray.direction);
            RaycastHit[] hits;
            if (true)//UNDONE CHECK BATTLE STATE TO MOVE
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hits = Physics.RaycastAll(ray);
                    foreach (RaycastHit hit in hits)
                    {
                        Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
                        if (hitTile != null)
                        {
                            BattleUnit selectedUnit = battleController.CurrentBattleOrder[0];
                            selectedUnit.CurrentPath = map.GeneratePathTo(hitTile.tileX, hitTile.tileZ, selectedUnit.tileX, selectedUnit.tileZ);
                        }
                    }
                }
            }
        }
    }
}
