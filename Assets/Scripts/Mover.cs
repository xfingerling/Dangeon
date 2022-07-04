using UnityEngine;

public class Mover : Fighter
{
    private Vector3 originalSize;

    protected BoxCollider2D _boxCollider;
    protected RaycastHit2D _raycastHit;
    protected Vector3 _moveDelta;

    public float ySpeed = 1.5f;
    public float xSpeed = 2f;


    protected virtual void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reset MoveDelta
        _moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction
        if (_moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (_moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        //Add push vector, if any
        _moveDelta += pushDirection;

        //Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Make sure we can move in this direction
        _raycastHit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, _moveDelta.y), Mathf.Abs(_moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_raycastHit.collider == null)
        {
            //Move
            transform.Translate(0, _moveDelta.y * Time.deltaTime, 0);
        }

        _raycastHit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(_moveDelta.x, 0), Mathf.Abs(_moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_raycastHit.collider == null)
        {
            //Move
            transform.Translate(_moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
