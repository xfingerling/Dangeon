using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private UIInterfaceInteractor _uiInterfaceInteractor;
    private Animator _inventoryPopupAnim;
    private Button _antiClickerButton;
    private UIInventorySlot[] _uiSlots;

    private void Start()
    {
        DeactivatePopup();

        Game.OnGameInitializedEvent += OnGameInitialized;
        _inventoryPopupAnim = GetComponent<Animator>();
        _antiClickerButton = GetComponentInChildren<Button>();

        _antiClickerButton.onClick.AddListener(CloseInventoryPopup);

        _uiSlots = GetComponentsInChildren<UIInventorySlot>();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;
        _uiInterfaceInteractor = Game.GetInteractor<UIInterfaceInteractor>();

        _uiInterfaceInteractor.inventory.OnInventoryStateChangeEvent += OnInventoryStateChange;

        SetupInventoryUI(_uiInterfaceInteractor.inventory);
    }

    public void DeactivatePopup()
    {
        gameObject.SetActive(false);
    }

    private void CloseInventoryPopup()
    {
        _inventoryPopupAnim.SetTrigger("Hide");
    }

    private void OnInventoryStateChange(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
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
}
