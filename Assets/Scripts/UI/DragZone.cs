using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragZone: MonoBehaviour, IDropHandler
{
    public int battlefieldIndex;

    void Awake()
    {
        battlefieldIndex = transform.GetSiblingIndex();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Dragable dropedUnit = eventData.pointerDrag.GetComponent<Dragable>();
        Dragable currentUnit = GetComponentInChildren<Dragable>();

        if (currentUnit != null)
        {
            currentUnit.parentToReturn = dropedUnit.parentToReturn;
            currentUnit.unit.isOnBattlefield = dropedUnit.unit.isOnBattlefield;
            currentUnit.unit.battlefieldIndex = dropedUnit.unit.battlefieldIndex;
            currentUnit.transform.SetParent(currentUnit.parentToReturn);
        }
        
        dropedUnit.parentToReturn = transform;
        dropedUnit.unit.isOnBattlefield = true;
        dropedUnit.unit.battlefieldIndex = battlefieldIndex;
    }
}
