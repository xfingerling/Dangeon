using UnityEngine;

public class Collectabe : Collidable
{

    protected bool isCollected;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            OnCollect();
    }

    protected virtual void OnCollect()
    {

    }
}
