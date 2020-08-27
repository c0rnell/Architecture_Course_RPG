using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public class ui_selection_cursor
{
    [Test]
    public void with_no_item_selected_shows_no_icon()
    {
        var uiCursor = inventory_helpers.GetSelectionCursor();
        Assert.IsFalse(uiCursor.IconVisible);
    }
    
    [Test]
    public void with_item_selected_shows_icon()
    {
        var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
        var uiCursor = inventory_helpers.GetSelectionCursor();
        inventoryPanel.Slots.First().OnPointerClick(null);
        
        Assert.IsTrue(uiCursor.IconVisible);
    }
}

    public class inventory_panel
    {
        [Test] 
        public void has_25_slots()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            Assert.AreEqual(Inventory.DefaultInventorySize, inventoryPanel.SlotCount);
        }
        
        [Test]
        public void bound_to_empty_inventory_has_all_slots_empty()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            var invnetory = inventory_helpers.GetInventory(0);

            inventoryPanel.Bind(invnetory);

            foreach (UIInventorySlot slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void bound_to_inventory_with_one_item_fills_only_first_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            var invnetory = inventory_helpers.GetInventory(1);

            var item= inventory_helpers.GetItem();
            invnetory.Pickup(item);
            
            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            
            inventoryPanel.Bind(invnetory);

            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }
        
        [Test]
        public void bound_to_inventory_fills_slot_for_each_item([NUnit.Framework.Range(0, 25)] int numberOfItems)
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            var invnetory = inventory_helpers.GetInventory(numberOfItems);
            
            foreach (UIInventorySlot slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }

            inventoryPanel.Bind(invnetory);

            for (var index = 0; index < inventoryPanel.SlotCount; index++)
            {
                var shouldBeEmpty = index >= numberOfItems;
                UIInventorySlot slot = inventoryPanel.Slots[index];
                Assert.AreEqual(shouldBeEmpty, slot.IsEmpty);
            }
        }
        
        [Test]
        public void places_item_in_slot_when_item_added_to_inventory()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            var invnetory = inventory_helpers.GetInventory();

            var item = inventory_helpers.GetItem();

            inventoryPanel.Bind(invnetory);

            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            
            invnetory.Pickup(item);
            
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }

        [Test]
        public void bound_to_null_inventory_has_empty_slots()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            inventoryPanel.Bind(null);
            
            foreach (UIInventorySlot slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }
        
        [Test]
        public void bound_to_valid_inventory_then_bound_to_null_inventory_has_empty_slots()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            var invventory = inventory_helpers.GetInventory(1);
            inventoryPanel.Bind(invventory);
            
            inventoryPanel.Bind(null);
            foreach (UIInventorySlot slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }
        
        [Test]
        public void updates_slots_when_items_are_moved()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems();
            var invventory = inventory_helpers.GetInventory(1);
            inventoryPanel.Bind(invventory);
            
            invventory.Move(0, 4);
            
            Assert.AreEqual(invventory.GetItemInSlot(4), inventoryPanel.Slots[4].Item);
        }
        
    }

    public static class inventory_helpers
    {
        public static Item GetItem()
        {
            var gameObjects = new GameObject("Item", typeof(SphereCollider));
            return gameObjects.AddComponent<Item>();
        }
        
        public static Inventory GetInventory(int numberOfItems = 0)
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = GetItem();
                inventory.Pickup(item);
            }

            return inventory;
        }
        public static UIInventoryPanel GetInventoryPanelWithItems(int numberOfItems = 0)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanel.prefab");
            var panel = GameObject.Instantiate(prefab);
            var inventory = GetInventory(numberOfItems);
            panel.Bind(inventory);
            return panel;
        }

        public static UISelectionCursor GetSelectionCursor()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UISelectionCursor>("Assets/Prefabs/UI/SelectionCursor.prefab");
            return GameObject.Instantiate(prefab);
        }
    }