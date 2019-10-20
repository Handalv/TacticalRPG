using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile_Asset")]
public class TileType : ScriptableObject
{
    new public string name = "New tile";
    public Material material = null;
    public bool isWalkable = true;
    public float movementCost = 2f;
}
