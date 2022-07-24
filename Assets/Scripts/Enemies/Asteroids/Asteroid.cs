using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : PooledObject<Asteroid>, IDamageable, IGameViewSubscribable
{
    public event Action OnDiedEvent;
    
    [SerializeField] private Sprite[] _sprites;

    [SerializeField] private float _speed = 50f;
    [SerializeField] private int _startHealthPoints = 20;
    [SerializeField] private int _damage = 20;

    [SerializeField] private float _size = 1f;
    [SerializeField] private float _maxSize = 1.1f;
    [SerializeField] private float _minSize = 0.5f;
    [SerializeField] private int _fracturePeaces = 2;
    
    private float _fractureSizeBoundary = 0.6f;
    private GameplayView _gameplayView;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    
    private HealthPoints _healthPoints;
    private DamageTextComposite _damageTextComposite;

    #region MONO

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        _healthPoints = new HealthPoints(_startHealthPoints);
    }

    private void Start()
    {
        SetRandomSprite();
    }

    private void OnEnable()
    {
        _healthPoints.OnDied += OnDied;
    }

    private void OnDisable()
    {
        _healthPoints.OnDied -= OnDied;
    }

    #endregion

    public void SetDependencies(DamageTextComposite damageTextComposite)
    {
        _damageTextComposite = damageTextComposite;
    }
    
    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;
        _size = HelperMethods.GetRandomSize(_minSize, _maxSize);
        _transform.localScale = Vector3.one * _size;
    }

    public void PrepareToFracture(Vector3 position, Quaternion rotation, float size)
    {
        _transform.position = position;
        _transform.rotation = rotation;
        _size = size;
        _transform.localScale = Vector3.one * size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);
        _rigidbody.AddTorque(direction.x * _speed);
    }
    
    public void TakeDamage(int damage)
    {
        _healthPoints.TakeDamage(damage);
    }

    private void SetRandomSprite()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        _transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
    }

    private void OnDied()
    {
        GetAsteroidFracturer().Fracture(_fracturePeaces);
        OnDiedEvent?.Invoke();
        UnSubscribeFromGameView(_gameplayView);
        ResetObject();
    }

    protected override void ResetObject()
    {
        _healthPoints.ResetHealthPoints(_startHealthPoints);
        ReturnToPool(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            _damageTextComposite.ShowDamageText(_damage, false, col.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (HelperMethods.IsObjectOutOfGameAre(col, gameObject))
        {
            ResetObject();
        }
    }

    private IFracturable GetAsteroidFracturer()
    {
        return new AsteroidFracturer(
            Pool,
            _transform,
            _size,
            _speed,
            _fractureSizeBoundary,
            _minSize,
            _gameplayView,
            _damageTextComposite);
    }

    public void SubscribeToGameView(GameplayView view)
    {
        if (OnDiedEvent != null)
            return;
        
        OnDiedEvent += view.IncreaseAsteroidKillScore;
        _gameplayView = view;
    }

    public void UnSubscribeFromGameView(GameplayView view)
    {
        OnDiedEvent -= view.IncreaseAsteroidKillScore;
    }
}