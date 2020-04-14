
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;


public class inventory_panel
    {
        [Test] 
        public void has_25_slots()
        {
            var inventoryPanel = GetInventoryPanel();
            Assert.AreEqual(Inventory.DefaultInventorySize, inventoryPanel.SlotCount);
        }
        
        [Test]
        public void bound_to_empty_inventory_has_all_slots_empty()
        {
            var inventoryPanel = GetInventoryPanel();
            var invnetory = GetInventory(0);

            inventoryPanel.Bind(invnetory);

            foreach (UIInventorySlot slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [Test]
        public void bound_to_inventory_with_one_item_fills_only_first_slot()
        {
            var inventoryPanel = GetInventoryPanel();
            var invnetory = GetInventory(1);

            var item= GetItem();
            invnetory.Pickup(item);
            
            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            
            inventoryPanel.Bind(invnetory);

            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }
        
        [Test]
        public void bound_to_inventory_fills_slot_for_each_item([NUnit.Framework.Range(0, 25)] int numberOfItems)
        {
            var inventoryPanel = GetInventoryPanel();
            var invnetory = GetInventory(numberOfItems);
            
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
            var inventoryPanel = GetInventoryPanel();
            var invnetory = GetInventory();

            var item = GetItem();

            inventoryPanel.Bind(invnetory);

            Assert.IsTrue(inventoryPanel.Slots[0].IsEmpty);
            
            invnetory.Pickup(item);
            
            Assert.IsFalse(inventoryPanel.Slots[0].IsEmpty);
        }

        [Test]
        public void bound_to_null_inventory_has_empty_slots()
        {
            var inventoryPanel = GetInventoryPanel();
            inventoryPanel.Bind(null);
            
            foreach (UIInventorySlot slot in inventoryPanel.Slots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }
        
        [Test]
        public void bound_to_valid_inventory_then_bound_to_null_inventory_has_empty_slots()
        {
            var inventoryPanel = GetInventoryPanel();
            var invventory = GetInventory(1);
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
            var inventoryPanel = GetInventoryPanel();
            var invventory = GetInventory(1);
            inventoryPanel.Bind(invventory);
            
            invventory.Move(0, 4);
            
            Assert.AreEqual(invventory.GetItemInSlot(4), inventoryPanel.Slots[4].Item);
        }
        

        private Item GetItem()
        {
            var gameObjects = new GameObject("Item", typeof(SphereCollider));
            return gameObjects.AddComponent<Item>();
        }
        
        private Inventory GetInventory(int numberOfItems = 0)
         {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = GetItem();
                inventory.Pickup(item);
            }

            return inventory;
         }

        public static UIInventoryPanel GetInventoryPanel()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/InventoryPanel.prefab");
            return GameObject.Instantiate(prefab);
        }
    }