using UnityEngine;

public class Mover : Fighter
{
    private Vector3 originalSize;

    //Animation
    protected Animator anim;
    protected bool isRunning;

    protected BoxCollider2D boxCollider;
    protected RaycastHit2D raycastHit;
    protected Vector3 moveDelta;

    public float ySpeed = 1.5f;
    public float xSpeed = 2f;


    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
        anim = GetComponent<Animator>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        anim.SetTrigger("Run");

        //Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        //Add push vector, if any
        moveDelta += pushDirection;

        //Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Make sure we can move in this direction
        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (raycastHit.collider == null)
        {
            //Move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (raycastHit.collider == null)
        {
            //Move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
