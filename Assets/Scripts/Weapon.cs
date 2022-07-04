using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    [SerializeField] private int[] _damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    [SerializeField] private float[] _pushForce = { 2, 2.2f, 2.5f, 3, 3.2f, 3.6f, 4f };

    //Upgrade
    public int WeaponLevel { get { return _weaponLevel; } private set { _weaponLevel = value; } }

    private int _weaponLevel = 0;
    private SpriteRenderer _spriteRenderer;

    //Swing 
    private Animator _anim;
    private float _cooldown = 0.5f;
    private float _lastSwing;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - _lastSwing > _cooldown)
            {
                _lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.tag == "Fighter")
        {
            if (collider.name == "Player")
                return;

            //Create a new damage object, then we'll send it to the fighter we'll hit
            Damage dmg = new Damage()
            {
                damageAmount = _damagePoint[WeaponLevel],
                origin = transform.position,
                pushForce = _pushForce[WeaponLevel],
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        _anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        WeaponLevel++;
        _spriteRenderer.sprite = GameManager.instance.weaponSprites[WeaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        WeaponLevel = level;
        _spriteRenderer.sprite = GameManager.instance.weaponSprites[WeaponLevel];
    }
}
