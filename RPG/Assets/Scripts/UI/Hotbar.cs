using NSubstitute.Core;
using UnityEngine;

public class Hotbar : MonoBehaviour
    {
        private Inventory m_Inventory;
        private Slot[] m_Slots;

        private void OnEnable()
        {
            m_Inventory = FindObjectOfType<Inventory>();
            m_Inventory.ItemPickedUp += ItemPickedUp;
            m_Slots = GetComponentsInChildren<Slot>();
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