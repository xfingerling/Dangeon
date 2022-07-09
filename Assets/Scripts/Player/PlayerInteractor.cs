using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        Player = Resources.Load<Player>("Player/Player");
        Object.Instantiate(Player);
    }
}
