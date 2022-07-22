
public class LightShipChasePlayerState : ChasePlayerState
{
    private LightShip _lightShip;
    
    public LightShipChasePlayerState(Enemy enemy, StateMachine stateMachine, LightShip lightShip) : base(enemy, stateMachine)
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
        
        if (isPlayerInsideAttackRange)
        {
            _stateMachine.ChangeState(_lightShip.AttackState);
        }
        else if (isPlayerInsideViewRange == false)
        {
            _stateMachine.ChangeState(_lightShip.LookForPlayerState);
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
