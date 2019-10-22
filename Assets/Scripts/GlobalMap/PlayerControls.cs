
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GlobalMap map;
    public Unit playerUnit;

    void Start()
    {
        map = GlobalMap.instance;
        //playerUnit = map.selectedUnit.GetComponent<Unit>();
    }

    void Update()
    {
        //TODO Check overlay pointer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction);
        RaycastHit[] hits;
        if (!GlobalMap.instance.GAMEPAUSED)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
                    if (hitTile != null)
                    {
                        map.GeneratePathTo(hitTile.tileX, hitTile.tileZ);
                    }
                }
            }
        }
        //TEST instant step
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerUnit.movementCD = 0;
        }
    }
}

