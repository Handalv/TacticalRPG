using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public string nickname;
    public int visionRange = 1;
    public int gold;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}

    private void Update()
    {
        //UNDONE RAYCAST

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
                        if (!hitTile.warFogEnabled)
                            GlobalMap.instance.MoveUnit(hitTile.tileX, hitTile.tileZ);
                }
            }
        }
    }
}

