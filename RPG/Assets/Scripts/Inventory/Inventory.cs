using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int DefaultInventorySize = 25;
    public event Action<Item> ActiveItemChange;
    public event Action<Item> ItemPickedUp;
    
    public event Action<int> ItemSlotChanged;

    [SerializeField] 
    private Transform m_RightHand;
    
    private Item[] m_Items = new Item[DefaultInventorySize];
    private Transform m_ItemRoot;

    public Item ActiveItem { get; private set; }
    public List<Item> Items => m_Items.ToList();
    public int Count => m_Items.Count(tag => tag != null);

    private void Awake()
    {
        m_ItemRoot = new GameObject("Items").transform;
        m_ItemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item, int? slot = null)
    {
        if (slot.HasValue == false)
            slot = FindFirstAvailableSlot();
        if (slot.HasValue == false)
            return;
        m_Items[slot.Value] = item;
        item.transform.SetParent(m_ItemRoot);
        ItemPickedUp?.Invoke(item);
        item.WasPickedUp = true;
        
        Equip(item);
    }

    private int? FindFirstAvailableSlot()
    {
        for (int i = 0; i < m_Items.Length; i++)
        {
            if (m_Items[i] == null)
                return i;
        }

        return null;
    }

    public void Equip(Item item)
    {
        if (ActiveItem != null)
        {
            ActiveItem.transform.SetParent(m_ItemRoot);
            ActiveItem.gameObject.SetActive(false);
        }
        
        Debug.Log($"Equiped item {item.gameObject.name}");
        item.transform.SetParent(m_RightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;
        ActiveItem.gameObject.SetActive(true);
        ActiveItemChange?.Invoke(ActiveItem);
    }

    public Item GetItemInSlot(int slot)
    {
        return m_Items[slot];
    }

    public void Move(int sourceSlot, int destinationSlot)
    {
        var item = m_Items[destinationSlot];
        m_Items[destinationSlot] = m_Items[sourceSlot];
        m_Items[sourceSlot] = item;
        ItemSlotChanged?.Invoke(sourceSlot);
        ItemSlotChanged?.Invoke(destinationSlot);
    }
}