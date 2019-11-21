using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //public TileType TileType;
    public TileType type;
    public int tileX;
    public int tileZ;

    public TileMap map;

    public bool isWalkable = true;
    public float movementCost = 2f;
    public int BattleMovementCost = 1;

    public List<MapObject> mapObjects;

    public GameObject GFX;

    [SerializeField]
    private GameObject warFog;
    public bool WarFogEnabled
    {
        set
        {
            warFog.SetActive(value);
            foreach(MapObject obj in mapObjects)
            {
                obj.SetGraphicActive(!value);
            }
        }
        get
        {
            return warFog.activeSelf;
        }
    }

    [SerializeField]
    private GameObject highlight;
    public void HighlightEnable(Color color)
    {
        highlight.GetComponent<MeshRenderer>().material.color = color;
        highlight.SetActive(true);
        if (WarFogEnabled)
        {
            highlight.SetActive(false);
        }
    }

    public void HighlightDisable()
    {
        highlight.SetActive(false);
    }

    public void SetTypeChanges(TileType t)
    {
        type = t;
        GFX.GetComponent<MeshRenderer>().material = t.material;
        isWalkable = t.isWalkable;
        movementCost = t.movementCost;
    }

}
