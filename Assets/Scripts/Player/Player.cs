using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _speedY = 1;

    private BoxCollider2D _boxCollider;
    private Vector3 _originalSize;
    private float _horizontalInput;
    private float _verticalInput;
    private RaycastHit2D raycastHit;

    private void Start()
    {
        transform.position = Game.GetInteractor<SpawnPositionInteractor>().SpawpPosition.position;
        _boxCollider = GetComponent<BoxCollider2D>();
        _originalSize = transform.localScale;
    }

    private void Update()
    {
        HundleInput();

        Move(new Vector3(_horizontalInput * _speedX, _verticalInput * _speedY, 0));
    }

    private void Move(Vector3 input)
    {
        Vector3 moveDelta = new Vector3(input.x, input.y, 0);

        //Swap sprite dirrection 
        if (moveDelta.x > 0)
            transform.localScale = _originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(_originalSize.x * -1, _originalSize.y, 0);

        //Make sure we can move in this direction
        raycastHit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (raycastHit.collider == null)
        {
            //Move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        raycastHit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (raycastHit.collider == null)
        {
            //Move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private void HundleInput()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }
}
