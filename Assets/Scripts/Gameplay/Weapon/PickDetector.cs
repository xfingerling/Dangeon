using System;
using UnityEngine;

public class PickDetector : Collectabe
{
    public event Action<object> OnPickedWeaponEvent;

    protected override void OnCollect(Collider2D collider)
    {
        if (!isCollected)
        {
            isCollected = true;

            collider.GetComponent<ItemPicker>().Pick(this.GetComponent<PickableWeapon>());

            OnPickedWeaponEvent?.Invoke(collider);
            gameObject.SetActive(false);
        }

    }
}
