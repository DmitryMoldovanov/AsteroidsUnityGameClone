using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy _enemy;
    
    protected Vector3 _targetPosition;
    
    protected bool isPlayerInsideViewRange;
    protected bool isPlayerInsideAttackRange;

    public float stateEntryTime { get; private set; }

    public EnemyState(Enemy enemy, StateMachine stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
    }
    
    public override void Enter()
    {
        stateEntryTime = Time.time;
        
        DoChecks();
    }
    
    public override void Exit()
    {
        
    }
    
    public override void LogicUpdate(float deltaTime)
    {

    }
    
    public override void PhysicsUpdate(float fixedDeltaTime)
    {
        DoChecks();
    }

    protected override void DoChecks()
    {
        isPlayerInsideViewRange = _enemy.Collider.IsPlayerInsideViewRange();
        isPlayerInsideAttackRange = _enemy.Collider.IsPlayerInsideAttackRange();
    }
}
