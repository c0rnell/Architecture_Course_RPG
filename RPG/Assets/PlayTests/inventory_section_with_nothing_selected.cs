using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

    public class inventory_section_with_nothing_selected
    {
        [Test]
        public void click_non_empty_slot_selects_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var slot = inventoryPanel.Slots[0];
            slot.OnPointerClick(null);
        
            Assert.AreEqual(slot, inventoryPanel.Selected);
        }
        
        [Test]
        public void click_empty_slot_does_no_select_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var slot = inventoryPanel.Slots[0];
            slot.OnPointerClick(null);
        
            Assert.IsNull(inventoryPanel.Selected);
        }
    }
    
    public class inventory_section_with_slot_selected
    {
        [Test]
        public void click_slot_moves_selected_item_to_clicked_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(2);
            var slot0 = inventoryPanel.Slots[0];
            var slot1 = inventoryPanel.Slots[1];
            var item0 = slot0.Item;
            var item1 = slot1.Item;
            
            slot0.OnPointerClick(null);
            slot1.OnPointerClick(null);
            
            Assert.AreEqual(item0, slot1.Item);
            Assert.AreEqual(item1, slot0.Item);
        }
        
        [Test]
        public void click_slot_celar_selection()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(2);
            var slot0 = inventoryPanel.Slots[0];
            var slot1 = inventoryPanel.Slots[1];
            
            slot0.OnPointerClick(null);
            Assert.IsNotNull(inventoryPanel.Selected);
            slot1.OnPointerClick(null);
            Assert.IsNull(inventoryPanel.Selected);
   
        }
    }
