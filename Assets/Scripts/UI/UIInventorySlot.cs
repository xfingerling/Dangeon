using UnityEngine;
using UnityEngine.EventSystems;
public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem _uiInventoryItem;

    public IInventorySlot slot { get; private set; }

    private InventoryWithSlots _uiInventory;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        _uiInventory = Game.GetInteractor<UIInterfaceInteractor>().inventory;
        _uiInventoryItem = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetSlot(IInventorySlot newSlot)
    {
        slot = newSlot;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.slot;
        var inventory = _uiInventory;

        inventory.TransitFromSlotToSlot(this, otherSlot, slot);

        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (slot != null)
            _uiInventoryItem.Refresh(slot);
    }
}
