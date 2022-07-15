using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;

    public int capacity { get; set; }
    public bool isFull => _slots.All(slot => slot.isFool);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;
        _slots = new List<IInventorySlot>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();

        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }

        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOftype = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

        foreach (var slot in slotsOfType)
            allItemsOftype.Add(slot.item);


        return allItemsOftype.ToArray();
    }

    public IInventoryItem[] GetEquippedItems()
    {
        var requiredSlots = _slots.FindAll(slot => !slot.isEmpty && slot.item.isEquipped);
        var equippedItem = new List<IInventoryItem>();

        foreach (var slot in requiredSlots)
            equippedItem.Add(slot.item);

        return equippedItem.ToArray();
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }

    public int GetItemAmount(Type itemType)
    {
        int amount = 0;
        var allItemSlots = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

        foreach (var itemSlot in allItemSlots)
            amount += itemSlot.amount;

        return amount;
    }

    public bool HasItem(Type itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);
        return item != null;
    }

    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotWithItem = GetAllSlots(itemType);

        if (slotWithItem.Length == 0)
            return;

        var amountToRemove = amount;
        var count = slotWithItem.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            var slot = slotWithItem[i];

            if (slot.amount > amountToRemove)
            {
                slot.item.amount -= amountToRemove;

                if (slot.amount <= 0)
                    slot.Clear();

                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);

                break;
            }

            var amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);
        }
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFool);

        if (slotWithSameItemButNotEmpty != null)
            return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);

        var emptySlot = _slots.Find(slot => slot.isEmpty);

        if (emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);

        Debug.Log($"cannot add item {item.type}");
        return false;
    }

    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }

    private bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.amount <= item.maxItemsInInventorySlot;
        var amountToAdd = fits ? item.amount : item.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.amount = amountToAdd;

        if (slot.isEmpty)
            slot.SetItem(clonedItem);
        else
            slot.item.amount += amountToAdd;

        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);

        if (amountLeft <= 0)
            return true;

        item.amount = amountLeft;
        return TryToAdd(sender, item);
    }

}
