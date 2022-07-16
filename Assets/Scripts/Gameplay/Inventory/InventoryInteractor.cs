using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractor : Interactor
{
    private int _inventoryCapacity = 15;
    public InventoryWithSlots inventory { get; private set; }

    //TEST
    private InventoryItemInfo _steelSwordInfo;
    private InventoryItemInfo _twoHandedSwordInfo;
    private UIInventorySlot[] _uiSlots;
    private GameObject _prefabUIInterface;
    private GameObject _goUIInterface;


    public override void Initialize()
    {
        _steelSwordInfo = Resources.Load<InventoryItemInfo>("InventoryItemInfo_SteelSword");
        _twoHandedSwordInfo = Resources.Load<InventoryItemInfo>("InventoryItemInfo_TwoHandedSword");
        _prefabUIInterface = Resources.Load<GameObject>("UI/[INTERFACE]");
        _goUIInterface = Object.Instantiate(_prefabUIInterface);
        inventory = new InventoryWithSlots(_inventoryCapacity);
        _uiSlots = _goUIInterface.GetComponentsInChildren<UIInventorySlot>();
        inventory.OnInventoryStateChangeEvent += OnInventoryStateChange;
    }

    public override void OnStart()
    {
        FillSlots();
    }

    //TEST
    public void FillSlots()
    {
        var allSlots = inventory.GetAllSlots();
        var avaliableSlots = new List<IInventorySlot>(allSlots);

        var filledSlots = 5;

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomSteelSwordIntoRandomSlot(avaliableSlots);
            avaliableSlots.Remove(filledSlot);

            filledSlot = AddRandomTwoHandedSwordIntoRandomSlot(avaliableSlots);
            avaliableSlots.Remove(filledSlot);
        }

        SetupInventoryUI(inventory);
    }

    private IInventorySlot AddRandomSteelSwordIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var steelSword = new SteelSword(_steelSwordInfo);
        steelSword.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, steelSword);
        return rSlot;
    }

    private IInventorySlot AddRandomTwoHandedSwordIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var twoHandedSword = new TwoHandedSword(_twoHandedSwordInfo);
        twoHandedSword.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, twoHandedSword);
        return rSlot;
    }

    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;

        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];

            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryStateChange(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
}
