using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform parentWhenDrag = null;
    public Transform parentToReturn = null;
    public PlayerUnitStats unit;

    void Awake()
    {
        if (parentWhenDrag == null)
            parentWhenDrag = UIGlobalMap.instance.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(parentWhenDrag);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturn);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
