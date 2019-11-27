using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] 
    private Transform m_RightHand;
    
    private List<Item> m_Items;
    private Transform m_ItemRoot;
    
    public Item ActiveItem { get; private set; }

    private void Awake()
    {
        m_Items = new List<Item>();
        m_ItemRoot = new GameObject("Items").transform;
        m_ItemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item)
    {
        m_Items.Add(item);
        item.transform.SetParent(m_ItemRoot);

        Equip(item);
    }

    private void Equip(Item item)
    {
        Debug.Log($"Equiped item {item.gameObject.name}");
        item.transform.SetParent(m_RightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;
    }
}