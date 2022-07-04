using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform _lookAt;
    [SerializeField] private float _boundX = 0.15f;
    [SerializeField] private float _boundY = 0.05f;

    private void Start()
    {
        _lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //Check if we are inside the bounds on the X axis
        float deltaX = _lookAt.position.x - transform.position.x;
        if (deltaX > _boundX || deltaX < -_boundX)
        {
            if (transform.position.x < _lookAt.position.x)
            {
                delta.x = deltaX - _boundX;
            }
            else
            {
                delta.x = deltaX + _boundX;
            }
        }

        //Check if we are inside the bounds on the Y axis
        float deltaY = _lookAt.position.y - transform.position.y;
        if (deltaY > _boundY || deltaY < -_boundY)
        {
            if (transform.position.y < _lookAt.position.y)
            {
                delta.y = deltaY - _boundY;
            }
            else
            {
                delta.y = deltaY + _boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
