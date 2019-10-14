using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTile : MonoBehaviour
{
    //public TileType TileType;
    public TileType type;
    public int tileX;
    public int tileZ;
    public GlobalMap map;
    public bool isWalkable = true;
    public float movementCost = 5f;

    public List<MapObject> mapObjects;

    [SerializeField]
    private GameObject warFog;
    public bool warFogEnabled
    {
        set
        {
            warFog.SetActive(value);
            foreach(MapObject obj in mapObjects)
            {
                obj.graphic.SetActive(!value);
            }
        }
        get
        {
            return warFog.activeSelf;
        }
    }
    // Ёмаё я уже и забыл как ходьба реализована
    //TODO Raycast on Title layer instread this
    private void OnMouseUp()
    {
        if(!warFogEnabled)
            map.MoveUnit(tileX, tileZ);
    }
}
