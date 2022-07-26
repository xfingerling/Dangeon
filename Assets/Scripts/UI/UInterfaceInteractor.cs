using UnityEngine;

public class UIInterfaceInteractor : Interactor
{
    public UIInterface UIInterface => _goUIInterface;
    public InventoryWithSlots inventory { get; private set; }

    private int _inventoryCapacity = 15;
    private UIInterface _prefabUIInterface;
    private UIInterface _goUIInterface;

    //TEST
    private InventoryItemInfo _steelSwordInfo;
    private InventoryItemInfo _twoHandedSwordInfo;

    public override void Initialize()
    {
        //_steelSwordInfo = Resources.Load<InventoryItemInfo>("InventoryItemInfo_SteelSword");
        //_twoHandedSwordInfo = Resources.Load<InventoryItemInfo>("InventoryItemInfo_TwoHandedSword");

        _prefabUIInterface = Resources.Load<UIInterface>("UI/[INTERFACE]");
        _goUIInterface = Object.Instantiate(_prefabUIInterface);

        inventory = new InventoryWithSlots(_inventoryCapacity);
    }

    //TEST
    //public void FillSlots()
    //{
    //    var allSlots = inventory.GetAllSlots();
    //    var avaliableSlots = new List<IInventorySlot>(allSlots);

    //    var filledSlots = 5;

    //    for (int i = 0; i < filledSlots; i++)
    //    {
    //        var filledSlot = AddRandomSteelSwordIntoRandomSlot(avaliableSlots);
    //        avaliableSlots.Remove(filledSlot);

    //        filledSlot = AddRandomTwoHandedSwordIntoRandomSlot(avaliableSlots);
    //        avaliableSlots.Remove(filledSlot);
    //    }

    //    SetupInventoryUI(inventory);
    //}

    //private IInventorySlot AddRandomSteelSwordIntoRandomSlot(List<IInventorySlot> slots)
    //{
    //    var rSlotIndex = Random.Range(0, slots.Count);
    //    var rSlot = slots[rSlotIndex];
    //    var rCount = Random.Range(1, 4);
    //    var steelSword = new SteelSword(_steelSwordInfo);
    //    steelSword.state.amount = rCount;
    //    inventory.TryToAddToSlot(this, rSlot, steelSword);
    //    return rSlot;
    //}

    //private IInventorySlot AddRandomTwoHandedSwordIntoRandomSlot(List<IInventorySlot> slots)
    //{
    //    var rSlotIndex = Random.Range(0, slots.Count);
    //    var rSlot = slots[rSlotIndex];
    //    var rCount = Random.Range(1, 4);
    //    var twoHandedSword = new TwoHandedSword(_twoHandedSwordInfo);
    //    twoHandedSword.state.amount = rCount;
    //    inventory.TryToAddToSlot(this, rSlot, twoHandedSword);
    //    return rSlot;
    //}




}
