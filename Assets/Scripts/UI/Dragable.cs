using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //mb just use canvas to do this class free to use for every UI elements
    public Transform parentWhenDrag = null;
    public Transform parentToReturn = null;
    public PlayerUnitStats unit;

    void Awake()
    {
        if (parentWhenDrag = null)
            parentWhenDrag = UIGlobalMap.instance.unitsListPanel.transform;
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
