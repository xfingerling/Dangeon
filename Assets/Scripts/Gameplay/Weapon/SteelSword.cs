using System;

public class SteelSword : IInventoryItem
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Type type { get; }

    public SteelSword(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clone = new SteelSword(info);
        clone.state.amount = state.amount;
        return clone;
    }
}
