using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }
    public int Health => _playerRepository.Health;
    public int MaxHealth => _playerRepository.MaxHealth;

    private PlayerRepository _playerRepository;
    private FloatingTextInteractor _floatingTextInteractor;

    public override void Initialize()
    {
        base.Initialize();
        _playerRepository = Game.GetRepository<PlayerRepository>();
        _floatingTextInteractor = Game.GetInteractor<FloatingTextInteractor>();

        Player playerPrefab = Resources.Load<Player>("Player/Player");
        Player = Object.Instantiate(playerPrefab);
    }

    public void MakeDamage(object sender, Damage damage)
    {
        _playerRepository.Health -= damage.damageAmount;

        //Push dirrection
        Vector3 pushDirection = (Player.transform.position - damage.origin).normalized * damage.pushForce;
        Player.Push(pushDirection);
    }

    public void Healing(object sender, int healingPower)
    {
        if (Health >= MaxHealth)
            return;

        _playerRepository.Health += healingPower;

        //UI
        _floatingTextInteractor.Show($"+{healingPower}HP", 15, Color.green, Player.transform.position, Vector3.up * 50, 1f);
    }
}