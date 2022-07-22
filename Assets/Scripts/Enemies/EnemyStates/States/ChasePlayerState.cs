
using UnityEngine;

public class ChasePlayerState : EnemyState
{
   
    public ChasePlayerState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        
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
        
        _targetPosition = _enemy.Collider.GetTargetPosition();

        _enemy.Movement.Rotate(_targetPosition);
        _enemy.Movement.Move(fixedDeltaTime);
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
