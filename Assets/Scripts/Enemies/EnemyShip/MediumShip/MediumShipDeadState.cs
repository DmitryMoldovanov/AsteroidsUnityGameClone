
public class MediumShipDeadState : DeadState
{
    private MediumShip _mediumShip;
    
    public MediumShipDeadState(Enemy enemy, StateMachine stateMachine, MediumShip mediumShip) : base(enemy, stateMachine)
    {
        _mediumShip = mediumShip;
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
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
