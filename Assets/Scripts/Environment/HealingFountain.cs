using UnityEngine;

public class HealingFountain : Collidable
{
    [SerializeField] private int _healingPower = 1;

    private PlayerInteractor _playerInteractor;
    private FloatingTextInteractor _floatingTextInteractor;
    private float _lastHealing;
    private float _cooldownHealing = 0.5f;

    protected override void Start()
    {
        base.Start();
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;
        _playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _floatingTextInteractor = Game.GetInteractor<FloatingTextInteractor>();
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (Time.time - _lastHealing > _cooldownHealing)
            {
                _lastHealing = Time.time;

                _playerInteractor.Healing(this, _healingPower);
            }
        }
    }
}
