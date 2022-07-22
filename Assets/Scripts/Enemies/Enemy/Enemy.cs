using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : PooledObject<Enemy>, IDamageable, IGameViewSubscribable, IPauseHandler
{
    public event Action OnDiedAction;
    
    [SerializeField] private int _startHealthPoints;
    [SerializeField] protected int _damage;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _viewRange;
    [SerializeField] private float _attackRange = .5f;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] protected int _attackRate;

    [Header("Dependencies")]
    [SerializeField] private ImageSpawner _imageSpawner;

    protected IWeapon _weapon;
    protected HealthPoints _healthPoints;
    protected StateMachine _stateMachine;
    protected DamageTextComposite _damageTextComposite;
    protected Rigidbody2D _rigidbody;
    
    private Transform _transform;
    private GameplayView _gameplayView;
    
    private Vector2 _pointToPatrolAround;

    public IMovable Movement { get; private set; }
    public IPatrolable Patrol { get; private set; }
    public ICollidable Collider { get; private set; }
    public bool CanAttack { get; private set; }
    public bool IsGamePaused { get; private set; }
    public ImageSpawner ImageSpawner => _imageSpawner;
    public Transform Transform => _transform;

    #region MONO

    protected virtual void Initialize()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _healthPoints = new HealthPoints(_startHealthPoints);
        Movement = new EnemyMovement(_transform, _rigidbody, _moveSpeed);
        Patrol = new EnemyPatrol(_transform, _turnSpeed);
        Collider = new EnemyCollider(_transform, _viewRange, _attackRange, _playerMask);
        _stateMachine = new StateMachine();
    }

    #endregion

    public void SetDependencies(DamageTextComposite damageTextComposite)
    {
        _damageTextComposite = damageTextComposite;
        _weapon = new MachineGun(Transform, _damageTextComposite);
    }
    
    private void Update()
    {
        _stateMachine.CurrentEntityState.LogicUpdate(Time.deltaTime);
    }
    
    private void FixedUpdate()
    {
        _stateMachine.CurrentEntityState.PhysicsUpdate(Time.fixedDeltaTime);
    }
    
    public void TakeDamage(int damage)
    {
        _healthPoints.TakeDamage(damage);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            _damageTextComposite.ShowDamageText(_damage, false, player.transform.position);
        }
    }

    public void PrepareToSpawn(Vector2 spawnPosition)
    {
        _transform.position = spawnPosition;
    }

    public void ToggleAttackState()
    {
        CanAttack = !CanAttack;
    }
    
    protected override void ResetObject()
    {
        OnDiedAction?.Invoke();
        UnSubscribeFromGameView(_gameplayView);
        GameContext.Instance.PauseManager.UnRegister(this);
    }
    
    public void SubscribeToGameView(GameplayView view)
    {
        if (OnDiedAction != null)
            return;
        
        OnDiedAction += view.IncreaseEnemyKillScore;
        _gameplayView = view;
        
        GameContext.Instance.PauseManager.Register(this);
    }

    public void UnSubscribeFromGameView(GameplayView view)
    {
        OnDiedAction -= view.IncreaseEnemyKillScore;
    }
    
    public void SetPause(bool isPaused)
    {
        IsGamePaused = isPaused;
    }

    #region GIZMOS

    protected void OnDrawGizmos()
    {
        DrawViewRange();
        DrawAttackRange();
        DrawForwardLine();
    }

    private void DrawViewRange()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRange);
    }
    
    private void DrawAttackRange()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void DrawForwardLine()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right);
    }

    #endregion
}
