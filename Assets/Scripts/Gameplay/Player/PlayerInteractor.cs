using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }
    public int Health => _playerRepository.Health;

    private PlayerRepository _playerRepository;

    public override void Initialize()
    {
        base.Initialize();
        _playerRepository = Game.GetRepository<PlayerRepository>();

        Player playerPrefab = Resources.Load<Player>("Player/Player");
        Player = Object.Instantiate(playerPrefab);
    }

    public void MakeDamage(object sender, Damage damage)
    {
        Debug.Log($"Make damage {damage.damageAmount}, player health {Health}");
        _playerRepository.Health -= damage.damageAmount;

        //Push dirrection
        Vector3 pushDirection = (Player.transform.position - damage.origin).normalized * damage.pushForce;
        Player.Push(pushDirection);
    }
}
