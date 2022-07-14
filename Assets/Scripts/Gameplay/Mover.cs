using UnityEngine;

public class Mover : MonoBehaviour
{
    private RaycastHit2D _raycastHit;
    private Vector3 _originalSize;
    private BoxCollider2D _boxCollider;
    private Vector3 _pushDirection;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _originalSize = transform.localScale;
    }

    protected virtual void Move(Vector3 input)
    {
        Vector3 moveDelta = new Vector3(input.x, input.y, 0);

        //Swap sprite dirrection 
        if (moveDelta.x > 0)
            transform.localScale = _originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(_originalSize.x * -1, _originalSize.y, 0);

        //Add push vector, if any
        moveDelta += _pushDirection;

        //Reduce push force every frame, based off recovery speed
        _pushDirection = Vector3.Lerp(_pushDirection, Vector3.zero, 0.2f);

        //Make sure we can move in this direction
        _raycastHit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_raycastHit.collider == null)
        {
            //Move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        _raycastHit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_raycastHit.collider == null)
        {
            //Move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    public void Push(Vector3 pushDirection)
    {
        _pushDirection = pushDirection;
    }
}
