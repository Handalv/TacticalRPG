using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class EnemyUnitList : MonoBehaviour
{
    public List<UnitPreset> Enemies;

    private GameObject UIelement;

    void Awake()
    {
        if (Enemies == null)
            Enemies = new List<UnitPreset>();
        
        UIelement = Instantiate(Resources.Load("UnitAmountText"), UIGlobalMap.instance.MapObjectElementsPanel.transform) as GameObject;
        gameObject.GetComponent<MapObject>().GraphicElements.Add(UIelement);
    }

    void LateUpdate()
    {
        if (UIelement.GetComponent<TextMeshProUGUI>().gameObject.activeSelf)
        {
            UIelement.GetComponent<TextMeshProUGUI>().text = "(" + Enemies.Count + ")";
            UIelement.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }

    public void AddUnit(UnitPreset unit)
    {
        Enemies.Add(unit);
    }

    public void ClearUnitList()
    {
        Enemies.Clear();
    }
}