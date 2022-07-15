using UnityEngine;

public class EnemyHitbox : Collidable
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _cooldownAttack = 1;
    [SerializeField] private float _pushForce = 1;

    private PlayerInteractor _playerInteractor;
    private FloatingTextInteractor _floatingTextInteractor;
    private float _lastAttack;

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
            if (Time.time - _lastAttack > _cooldownAttack)
            {
                _lastAttack = Time.time;

                //Create a new damage object, before sending it to the player
                Damage dmg = new Damage()
                {
                    origin = transform.position,
                    damageAmount = _damage,
                    pushForce = _pushForce,
                };

                _playerInteractor.MakeDamage(this, dmg);

                //UI
                _floatingTextInteractor.Show($"{dmg.damageAmount} DMG", 10, Color.red, transform.position, Vector3.down * 30, 1.5f);
            }
        }
    }
}
