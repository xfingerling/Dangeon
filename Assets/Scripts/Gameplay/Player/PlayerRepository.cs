using UnityEngine;

public class PlayerRepository : Repository
{
    public int Health { get; set; }

    private int _defaultHealth = 10;

    public override void Initialize()
    {
        Health = _defaultHealth;
        Debug.Log($"Player health {Health}");
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {

    }
}
