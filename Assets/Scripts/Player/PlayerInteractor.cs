using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }

    private SpawnPositionInteractor _spawnPositionInteractor;

    public override void Initialize()
    {
        base.Initialize();

        Player playerPrefab = Resources.Load<Player>("Player/Player");
        Player = Object.Instantiate(playerPrefab);
    }
}
