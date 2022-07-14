using UnityEngine;

public class Chest : Collectabe
{
    [SerializeField] private int _coinsAmount;
    [SerializeField] private Sprite _emptyChest;

    private FloatingTextInteractor _floatingTextInteractor;
    private bool _isCollected;
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;
        _floatingTextInteractor = Game.GetInteractor<FloatingTextInteractor>();
    }

    protected override void OnCollect()
    {
        if (!_isCollected)
        {
            _isCollected = true;

            _spriteRenderer.sprite = _emptyChest;

            _floatingTextInteractor.Show($"+ {_coinsAmount} coins", 20, Color.yellow, transform.position, Vector3.up * 40, 3);
        }
    }
}
