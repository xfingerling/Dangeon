using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float immuneTime = 1;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;

    //All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText($"{dmg.damageAmount.ToString()}dmg", 20, Color.red, transform.position, Vector3.up * 20, 1f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}