
public class LightShipIdleState : IdleState
{
    private LightShip _lightShip;
    
    public LightShipIdleState(Enemy enemy, StateMachine stateMachine, LightShip lightShip) : base(enemy, stateMachine)
    {
        _lightShip = lightShip;
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
        
        if (isPlayerInsideViewRange)
        {
            _stateMachine.ChangeState(_lightShip.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            _stateMachine.ChangeState(_lightShip.PatrolState);
        }
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
