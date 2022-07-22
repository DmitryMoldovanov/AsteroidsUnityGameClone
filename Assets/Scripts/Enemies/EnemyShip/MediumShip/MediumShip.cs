
public class MediumShip : MeleeEnemy
{
    public MediumShipIdleState IdleState { get; private set; }
    public MediumShipPatrolState PatrolState { get; private set; }
    public MediumShipLookForPlayerState LookForPlayerState { get; private set; }
    public MediumShipPlayerDetectedState PlayerDetectedState { get; private set; }

    public MediumShipAttackState AttackState { get; private set; }

    public MediumShipChasePlayerState ChasePlayerState { get; private set; }

    public MediumShipDeadState DeadState { get; private set; }

    private void Awake()
    {
        base.Initialize();

        IdleState = new MediumShipIdleState(this, _stateMachine, this);
        PatrolState = new MediumShipPatrolState(this, _stateMachine, this);
        LookForPlayerState = new MediumShipLookForPlayerState(this, _stateMachine, this);
        PlayerDetectedState = new MediumShipPlayerDetectedState(this, _stateMachine, this);
        ChasePlayerState = new MediumShipChasePlayerState(this, _stateMachine, this);
        AttackState = new MediumShipAttackState(this, _stateMachine, this);
        DeadState = new MediumShipDeadState(this, _stateMachine, this);
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