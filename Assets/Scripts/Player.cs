using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;

        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;

        string msg = $"+{healingAmount}hp";
        GameManager.instance.ShowText(msg, 25, Color.green, transform.position, Vector2.up * 40, 1f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
