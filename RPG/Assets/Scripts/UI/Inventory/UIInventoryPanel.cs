using System;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
    {
        public event Action OnSelectionChanged;
        
        private UIInventorySlot[] m_Slots;
        public UIInventorySlot[] Slots => m_Slots;
        
        public int SlotCount => m_Slots.Length;
        public UIInventorySlot Selected { get; private set; }



        private Inventory m_BoundInvnetory;
        
        private void Awake()
        {
            m_Slots = GetComponentsInChildren<UIInventorySlot>();
            RegisterSlotsCallbacks();
        }

        private void RegisterSlotsCallbacks()
        {
            foreach (var slot in m_Slots)
            {
                slot.OnSlotClicked += HandleSlotClicked;
            }
        }

        private void HandleSlotClicked(UIInventorySlot slot)
        {
            if (Selected != null)
            {
                Swap(slot);
                Selected = null;
            }
            else if (slot.IsEmpty == false)
            {
                Selected = slot;
            }
            OnSelectionChanged?.Invoke();
        }

        private void Swap(UIInventorySlot slot)
        {
            m_BoundInvnetory.Move(GetSlotIndex(Selected), GetSlotIndex(slot));
        }

        private int GetSlotIndex(UIInventorySlot selected)
        {
            for (int i = 0; i < m_Slots.Length; i++)
            {
                if (m_Slots[i] == selected)
                    return i;
            }

            return -1;
        }

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
                m_BoundInvnetory.ItemPickedUp += HandleItemPickup;
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