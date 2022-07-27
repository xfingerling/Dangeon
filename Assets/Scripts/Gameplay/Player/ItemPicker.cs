using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public PickableItem PickableItem;

    private InventoryWithSlots _inventory;

    private void Start()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;
        _inventory = Game.GetInteractor<UIInterfaceInteractor>().inventory;
    }

    public void Pick(PickableItem item)
    {
        Debug.Log($"Pick {item.name}");
    }

    public void Pick(PickableWeapon weapon)
    {
        _inventory.TryToAdd(gameObject, weapon);
    }
}
