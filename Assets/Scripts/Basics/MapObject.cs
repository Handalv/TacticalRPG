using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public List<GameObject> GraphicElements;
    public int tileX;
    public int tileZ;
    public int visionRange = 0;

    //
    public Faction faction;


    [SerializeField]
    private GameObject highlight;

    void Start()
    {
        if(highlight == null)
        {
            GameObject go = Instantiate(Resources.Load("HighLight"),this.transform) as GameObject;
            go.SetActive(false);
            highlight = go;
        }
        if (GraphicElements.Count == 0)
        {
            Debug.Log(gameObject.name + " Graphic elements are missing");
        }
    }

    public void HighlightEnable(Color color)
    {
        highlight.GetComponent<MeshRenderer>().material.color = color;
        highlight.SetActive(true);
        if (GameSettings.instance.CurrentMap.tiles[tileX, tileZ].WarFogEnabled)
        {
            highlight.SetActive(false);
        }
    }

    public void HighlightDisable()
    {
        highlight.SetActive(false);
    }

    public void SetGraphicActive(bool value)
    {
        foreach(GameObject element in GraphicElements)
        {
            element.SetActive(value);
        }
    }
}
