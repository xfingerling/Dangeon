using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool isChasing;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D contactFilter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                isChasing = true;

            if (isChasing)
            {
                UpdateMotor((playerTransform.position - transform.position).normalized);
            }
            else
            {
                GoToStartPosition();
            }
        }
        else
        {
            GoToStartPosition();
            isChasing = false;
        }
    }

    private void GoToStartPosition()
    {
        if (Vector3.Distance(startingPosition, transform.position) > 0.1)
        {
            UpdateMotor((startingPosition - transform.position).normalized);
        }
        else
        {
            isRunning = false;
            anim.SetTrigger("Stand");
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText($"+{xpValue} xp", 20, Color.magenta, transform.position, Vector3.up * 40, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startingPosition, chaseLength);
        Gizmos.DrawWireSphere(startingPosition, triggerLength);
    }
}
