using UnityEngine;
using UnityEngine.UI;

public class UILayerHUD : MonoBehaviour
{
    [SerializeField] private UIInventory _uiInventoryPopup;

    private Button _inventoryButton;
    private UIInterface _uiInterface;

    private void Start()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;
        _uiInterface = Game.GetInteractor<UIInterfaceInteractor>().UIInterface;

        _inventoryButton = GetComponentInChildren<Button>();
        _inventoryButton.onClick.AddListener(OpenInventoryPopup);
    }

    private void OpenInventoryPopup()
    {
        _uiInventoryPopup.gameObject.SetActive(true);
    }
}
