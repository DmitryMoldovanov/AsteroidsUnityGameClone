
public class LightShip : RangeEnemy
{
    public LightShipIdleState IdleState { get; private set; }
    public LightShipLookForPlayerState LookForPlayerState { get; private set; }
    public LightShipPlayerDetectedState PlayerDetectedState { get; private set; }
    public LightShipChasePlayerState ChasePlayerState { get; private set; }
    public LightShipAttackState AttackState { get; private set; }
    public LightShipPatrolState PatrolState { get; private set; }
    public LightShipDeadState DeadState { get; private set; }

    private void Awake()
    {
        base.Initialize();
        
        AttackState = new LightShipAttackState(this, _stateMachine, this);
        IdleState = new LightShipIdleState(this, _stateMachine, this);
        PatrolState = new LightShipPatrolState(this, _stateMachine, this);
        LookForPlayerState = new LightShipLookForPlayerState(this, _stateMachine, this);
        PlayerDetectedState = new LightShipPlayerDetectedState(this, _stateMachine, this);
        ChasePlayerState = new LightShipChasePlayerState(this, _stateMachine, this);
        DeadState = new LightShipDeadState(this, _stateMachine, this);
    }
    
    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void OnEnable()
    {
        _healthPoints.OnDied += ResetObject;
        
        _stateMachine.Initialize(IdleState);
    }

    private void OnDisable()
    {
        _healthPoints.OnDied -= ResetObject;
    }
    
    protected override void ResetObject()
    {
        base.ResetObject();
        _stateMachine.ChangeState(DeadState);
    }
}
