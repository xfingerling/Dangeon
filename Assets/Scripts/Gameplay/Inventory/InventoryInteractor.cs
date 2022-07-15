using UnityEngine;

public class InventoryInteractor : Interactor
{
    private int _inventoryCapacity = 10;
    private IInventory _inventory;

    public override void Initialize()
    {
        _inventory = new InventoryWithSlots(_inventoryCapacity);
        Debug.Log($"Inventory initialized, capacity {_inventory.capacity}");
    }
}
