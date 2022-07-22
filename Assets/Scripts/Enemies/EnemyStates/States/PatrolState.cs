
using UnityEngine;

public class PatrolState : EnemyState
{
    private Vector2 _patrolPoint;
    
    public PatrolState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        
        _patrolPoint = _enemy.Patrol.CalculatePatrolPoint();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(float deltaTime)
    {
        base.LogicUpdate(deltaTime);
    }

    public override void PhysicsUpdate(float fixedDeltaTime)
    {
        base.PhysicsUpdate(fixedDeltaTime);
        
        _enemy.Patrol.Patrol(_patrolPoint, fixedDeltaTime);
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
