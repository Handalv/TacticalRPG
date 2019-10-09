using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    //public TileType TileType;
    public TileType type;
    public int tileX;
    public int tileZ;
    public GlobalMap map;
    public bool isWalkable = true;

    public List<MapObject> mapObjects;

    [SerializeField]
    private GameObject warFog;
    public bool warFogEnabled
    {
        set
        {
            warFog.SetActive(value);
        }
        get
        {
            return warFog.activeSelf;
        }
    }

    private void OnMouseUp()
    {
        if(!warFogEnabled)
            map.MoveUnit(tileX, tileZ);
    }
}
