using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : PooledObject<Bullet>, IProjectile
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private int _damage = 5;
    
    [Header("these two in percents")]
    [SerializeField] private int _criticalChance = 20;
    [SerializeField] private int _criticalMultiplayer = 25;
    
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private DamageTextComposite _damageTextComposite;
    private Transform _parent;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    public void OnDisable()
    {
        ResetObject();
    }

    public void Initialize(Transform parent, DamageTextComposite damageTextComposite)
    {
        _parent = parent;
        _damageTextComposite = damageTextComposite;
        _transform.position = parent.position;
        _transform.rotation = parent.rotation;
    }

    public void Shoot()
    {
        _rigidbody2D.AddForce(_parent.up * _speed);
    }
    
    protected override void ResetObject()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IDamageable damageable))
        {
            int damage = HelperMethods.CalculateDamage(out bool isCritical, _damage, _criticalChance, _criticalMultiplayer);
            
            damageable.TakeDamage(damage);
            _damageTextComposite.ShowDamageText(damage, isCritical, _transform.position);
            
            ReturnToPool(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (HelperMethods.IsObjectOutOfGameAre(other, gameObject))
        {
            ReturnToPool(this);
        }
    }
}