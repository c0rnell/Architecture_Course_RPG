using System.Collections.Generic;
using NSubstitute.ClearExtensions;
using UnityEngine;

    public class UIInventoryPanel : MonoBehaviour
    {
        private UIInventorySlot[] m_Slots;
        public UIInventorySlot[] Slots => m_Slots;

        private Inventory m_BoundInvnetory;
        
        private void Awake()
        {
            m_Slots = GetComponentsInChildren<UIInventorySlot>();
        }

        public int SlotCount => m_Slots.Length;

        public void Bind(Inventory inventory)
        {
            if (m_BoundInvnetory != null)
            {
                m_BoundInvnetory.ItemPickedUp -= HandleItemPickup;
                m_BoundInvnetory.ItemSlotChanged -= HandleItemSlotChanged;
            }

            if (inventory != null)
            {
                m_BoundInvnetory = inventory;
                inventory.ItemPickedUp += HandleItemPickup;
                m_BoundInvnetory.ItemSlotChanged += HandleItemSlotChanged;
                RefreshSlots();
            }
            else
            {
                ClearSlots();
            }
            
        }

        private void HandleItemSlotChanged(int slot)
        {
            Slots[slot].SetItem(m_BoundInvnetory.GetItemInSlot(slot));
        }

        private void ClearSlots()
        {
            foreach (var slot in Slots)
                slot.Clear();
        }

        private void RefreshSlots()
        {
            for (int i = 0; i < m_Slots.Length; i++)
            {
                if (m_BoundInvnetory.Items.Count > i)
                    m_Slots[i].SetItem(m_BoundInvnetory.Items[i]);
                else
                    m_Slots[i].Clear();
            }
        }

        private void HandleItemPickup(Item item)
        {
            RefreshSlots();
        }
    }