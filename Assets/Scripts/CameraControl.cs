using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform _playerPos;
    private Vector3 _target
    {
        get
        {
            if (_playerPos == null)
            {
                Player player = FindObjectOfType<Player>();
                if (player == null)
                {
                    return Vector3.zero;
                }
                else
                    _playerPos = player.transform;
            }

            return new Vector3(_playerPos.position.x, _playerPos.position.y, transform.position.z);
        }
    }
    private Vector3 _velocity;

    [SerializeField] private float _cameraSmoothness = 0.1f;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        transform.position = _target - _offset;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target - _offset, ref _velocity, _cameraSmoothness);
    }
}
