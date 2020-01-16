using System;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Inventory))]
    public class NpcLoot : MonoBehaviour
    {
        [SerializeField] private Item[] m_itemPrefabs;
        
        private Inventory m_Inventory;

        private void Awake()
        {
            m_Inventory = GetComponent<Inventory>();
        }

        private void Start()
        {
            foreach (Item itemPrefab in m_itemPrefabs)
            {
                var itemInstance = Instantiate(itemPrefab);
                m_Inventory.Pickup(itemInstance);
            }
        }
    }
}