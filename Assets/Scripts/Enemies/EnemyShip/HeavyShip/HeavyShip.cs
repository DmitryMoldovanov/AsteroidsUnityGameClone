
using UnityEngine;

public class HeavyShip : RangeEnemy
{
    public HeavyShipIdleState IdleState { get; private set; }
    public HeavyShipPatrolState PatrolState { get; private set; }
    public HeavyShipLookForPlayerState LookForPlayerState { get; private set; }
    public HeavyShipPlayerDetectedState PlayerDetectedState { get; private set; }
    public HeavyShipAttackState AttackState { get; private set; }
    public HeavyShipChasePlayerState ChasePlayerState { get; private set; }
    public HeavyShipDeadState DeadState { get; private set; }
    
    private void Awake()
    {
        base.Initialize();

        IdleState = new HeavyShipIdleState(this, _stateMachine, this);
        PatrolState = new HeavyShipPatrolState(this, _stateMachine, this);
        LookForPlayerState = new HeavyShipLookForPlayerState(this, _stateMachine, this);
        PlayerDetectedState = new HeavyShipPlayerDetectedState(this, _stateMachine, this);
        AttackState = new HeavyShipAttackState(this, _stateMachine, this);
        ChasePlayerState = new HeavyShipChasePlayerState(this, _stateMachine, this);
        DeadState = new HeavyShipDeadState(this, _stateMachine, this);
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
