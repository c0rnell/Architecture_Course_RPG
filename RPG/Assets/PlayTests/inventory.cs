using NUnit.Framework;
using UnityEngine;

public class inventory
{
    
    private Inventory GetInventory()
    {
        return new GameObject("INVENTORY").AddComponent<Inventory>();
    }
    
    // Add times
    [Test]
    public void can_add_items()
    {
        var inventory = GetInventory();
        
        Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
        inventory.Pickup(item);
        
        Assert.AreEqual(1, inventory.Count);
    }
    
    //place item to specific slot
    [Test]
    public void can_add_item_to_specific_slot()
    {
        var inventory = GetInventory();
        
        Item item = new GameObject("Item", typeof(SphereCollider)).AddComponent<Item>();
        inventory.Pickup(item, 5);
        
        Assert.AreEqual(item, inventory.GetItemInSlot(5));
    }
    
    // Change slots move
    
    [Test]
    public void can_move_item_to_empty_slot()
    {
        var inventory = GetInventory();
        
        Item item = new GameObject("Item", typeof(SphereCollider)).AddComponent<Item>();
        inventory.Pickup(item);
        
        Assert.AreEqual(item, inventory.GetItemInSlot(0));

        inventory.Move(0, 4);
        
        Assert.AreEqual(item, inventory.GetItemInSlot(4));
    }
    
    // Remove items
    // DropItems

    

    // Hotkey/hotbar assigment
    // change visuals
    // modify stats
    // Persist and load
}