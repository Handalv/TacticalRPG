using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIforInventory : MonoBehaviour, IDropHandler
{
    public GameObject ItemSlotsParent;
    public TextMeshProUGUI GoldText;
    public Inventory inventoryFrom;

    public void OnDrop(PointerEventData eventData)
    {
        ItemSlot itemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        itemSlot.parentToReturn = transform;

    }
}