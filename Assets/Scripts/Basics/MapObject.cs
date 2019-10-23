using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    //We can keep X and Z of this here to avoid of using ConvertWorldCoordToTile
    public List<GameObject> GraphicElements;
    public int tileX;
    public int tileZ;

    public int visionRange = 0;
    public Faction faction;

    void Start()
    {
        if (GraphicElements.Count == 0)
        {
            Debug.Log(gameObject.name + " Graphic elements are missing");
        }
    }

    public void SetGraphicActive(bool value)
    {
        foreach(GameObject element in GraphicElements)
        {
            element.SetActive(value);
        }
    }
}
