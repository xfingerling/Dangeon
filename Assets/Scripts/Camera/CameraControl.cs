using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _cameraSmoothness = 0.1f;
    [SerializeField] private Vector3 _offset;

    private Transform _playerTransform;
    private Vector3 _velocity;
    private Vector3 _target
    {
        get
        {
            if (_playerTransform == null)
                return Vector3.zero;
            else
                return new Vector3(_playerTransform.position.x, _playerTransform.position.y, transform.position.z);
        }
    }


    private void Start()
    {
        _playerTransform = Game.GetInteractor<PlayerInteractor>().Player.transform;
        transform.position = _target - _offset;
    }
    private void LateUpdate()
    {
        Debug.Log(_playerTransform.position);
        transform.position = Vector3.SmoothDamp(transform.position, _target - _offset, ref _velocity, _cameraSmoothness);
    }
}
