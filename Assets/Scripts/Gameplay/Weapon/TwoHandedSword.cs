using System;

public class TwoHandedSword : IInventoryItem
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Type type { get; }

    public TwoHandedSword(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clone = new TwoHandedSword(info);
        clone.state.amount = state.amount;
        return clone;
    }
}
