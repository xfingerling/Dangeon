using System;

public interface IInventoryItem
{
    bool isEquipped { get; set; }
    Type type { get; }
    int maxItemsInInventorySlot { get; }
    int amount { get; set; }

    IInventoryItem Clone();
}
