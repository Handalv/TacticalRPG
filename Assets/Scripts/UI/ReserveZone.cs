using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IPointerEnterHandler
public class ReserveZone : MonoBehaviour, IDropHandler 
{
    public void OnDrop(PointerEventData eventData)
    {
        Dragable dropedUnit = eventData.pointerDrag.GetComponent<Dragable>();
        dropedUnit.parentToReturn = transform;
        dropedUnit.unit.isOnBattlefield = false;
    }
}
