public class PlayerRepository : Repository
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    private int _defaultHealth = 10;

    public override void Initialize()
    {
        MaxHealth = _defaultHealth;
        Health = MaxHealth;
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
