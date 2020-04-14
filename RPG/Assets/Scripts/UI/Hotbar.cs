using System;
using UnityEngine;

public class Hotbar : MonoBehaviour
    {
        private Inventory m_Inventory;
        private Slot[] m_Slots;
        private Player m_Player;

        private void OnEnable()
        {
            m_Player = FindObjectOfType<Player>();
            PlayerInput.Instance.HotkeyPressed += OnHotkeyPressed;
            
            m_Inventory = FindObjectOfType<Inventory>();
            m_Inventory.ItemPickedUp += ItemPickedUp;
            m_Slots = GetComponentsInChildren<Slot>();
        }

        private void OnDisable()
        {
            PlayerInput.Instance.HotkeyPressed -= OnHotkeyPressed;
            m_Inventory.ItemPickedUp -= ItemPickedUp;
        }

        private void OnHotkeyPressed(int index)
        {
            if(index >= m_Slots.Length|| index < 0)
                return;
            
            if (m_Slots[index].IsEmpty == false)
            {
                m_Inventory.Equip(m_Slots[index].Item);
            }
        }

        private void ItemPickedUp(Item item)
        {
            Slot slot = FindNextOpenSlot();
            if (slot != null)
                slot.SetItem(item);
        }

        private Slot FindNextOpenSlot()
        {
            foreach (var slot in m_Slots)
            {
                if (slot.IsEmpty)
                    return slot;
            }

            return null;
        }
    }