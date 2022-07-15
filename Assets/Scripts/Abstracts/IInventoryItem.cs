using UnityEngine;

public interface IInventoryItem
{
    bool isEquipped { get; set; }
    Types type { get; }
    int maxItemsInInventorySlot { get; }
    int amount { get; set; }

    IInventoryItem Clone();
}
