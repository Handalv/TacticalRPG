using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Item item = null;
    public UIforInventory UI;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI costText;

    public Transform parentWhenDrag = null;
    public Transform parentToReturn = null;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        costText.text = item.Cost.ToString();
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        costText.text = "";
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
