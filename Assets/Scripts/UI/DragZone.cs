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
        UnitList unitList = UnitList.instance;
        Dragable dropedUnit = eventData.pointerDrag.GetComponent<Dragable>();
        Dragable currentUnit = GetComponentInChildren<Dragable>();


        if (currentUnit != null)
        {
            currentUnit.parentToReturn = dropedUnit.parentToReturn;
            unitList.isOnBattleField[currentUnit.IndexInUnitList] = unitList.isOnBattleField[dropedUnit.IndexInUnitList];
            unitList.BattleFieldIndex[currentUnit.IndexInUnitList] = unitList.BattleFieldIndex[dropedUnit.IndexInUnitList];
            currentUnit.transform.SetParent(currentUnit.parentToReturn);
        }
        
        dropedUnit.parentToReturn = transform;
        unitList.isOnBattleField[dropedUnit.IndexInUnitList] = true;
        unitList.BattleFieldIndex[dropedUnit.IndexInUnitList] = battlefieldIndex;
    }
}
