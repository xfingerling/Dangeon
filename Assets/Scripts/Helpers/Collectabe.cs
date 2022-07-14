using UnityEngine;

public class Collectabe : Collidable
{

    protected bool collected;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            OnCollect();
    }

    protected virtual void OnCollect()
    {

    }
}
