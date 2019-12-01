using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Item item = null;
    Inventory inventory;

    public Transform parentWhenDrag = null;
    public Transform parentToReturn = null;

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
