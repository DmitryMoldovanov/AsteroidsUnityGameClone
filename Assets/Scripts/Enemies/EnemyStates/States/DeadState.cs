
public class DeadState : EnemyState
{
    public DeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        
        _enemy.ReturnToPool(_enemy);
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
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
