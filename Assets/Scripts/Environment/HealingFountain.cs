using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;

    private float healCooldown = 1;
    private float lastHeal;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name != "Player")
            return;

        if (Time.time - lastHeal > healCooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
