
public class MediumShipPatrolState : PatrolState
{
    private MediumShip _mediumShip;
    
    public MediumShipPatrolState(Enemy enemy, StateMachine stateMachine, MediumShip mediumShip) : base(enemy, stateMachine)
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
        
        if (isPlayerInsideViewRange)
        {
            _stateMachine.ChangeState(_mediumShip.PlayerDetectedState);
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
