using System;

public class PickableWeapon : PickableItem, IInventoryItem
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Type type { get; }

    public PickableWeapon(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clone = new PickableWeapon(info);
        clone.state.amount = state.amount;
        return clone;
    }
}
