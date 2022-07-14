using UnityEngine;

public class Enemy : Mover
{
    [SerializeField] private float _triggerLength;
    [SerializeField] private float _chaseLength;
    [SerializeField] private float _chasingSpeed = 1;
    [SerializeField] private int _hitpoint;

    private Transform _playerTransform;
    private Vector3 _startPosition;
    private bool _isChasing;

    private void Start()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;

        _startPosition = transform.position;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        _playerTransform = Game.GetInteractor<PlayerInteractor>().Player.transform;
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null)
            return;

        float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized * _chasingSpeed;


        if (distanceToPlayer < _chaseLength)
        {
            if (distanceToPlayer < _triggerLength)
                _isChasing = true;

            if (_isChasing)
                Move(directionToPlayer);
            else
                GoToStartPosition();
        }
        else
        {
            _isChasing = false;
            GoToStartPosition();
        }
    }

    private void GoToStartPosition()
    {
        Vector3 directionToStartPosition = (_startPosition - transform.position).normalized;

        if (Vector3.Distance(_startPosition, transform.position) > 0.1)
        {
            Move(directionToStartPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _triggerLength);
        Gizmos.DrawWireSphere(transform.position, _chaseLength);
    }
}
