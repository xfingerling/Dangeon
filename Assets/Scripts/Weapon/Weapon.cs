using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;

    //Damage struct
    [SerializeField] protected int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    [SerializeField] protected float pushForce = 7;
    [SerializeField] private float _cooldown = 0.3f;

    //Upgrade
    public int WeaponLevel { get { return _weaponLevel; } private set { _weaponLevel = value; } }

    private int _weaponLevel = 0;
    private SpriteRenderer _spriteRenderer;

    //Attack 
    private Animator _anim;
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
                Attack();
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
                damageAmount = damagePoint[WeaponLevel],
                origin = transform.position,
                pushForce = pushForce,
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Attack()
    {
        _anim.SetTrigger("Attack");
    }

    public void UpgradeWeapon()
    {
        WeaponLevel++;
        _spriteRenderer.sprite = GameManager.instance.weapon.weaponSprites[WeaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        WeaponLevel = level;
        _spriteRenderer.sprite = GameManager.instance.weapon.weaponSprites[WeaponLevel];
    }
}
