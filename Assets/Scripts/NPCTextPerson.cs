using UnityEngine;

public class NPCTextPerson : Collidable
{
    public string message;

    private float cooldown = 4f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            if (Time.time - lastShout > cooldown)
            {
                lastShout = Time.time;
                GameManager.instance.ShowText(message, 30, Color.white, transform.position + Vector3.up, Vector3.zero, cooldown);
            }
        }
    }
}
