
public class MediumShipChasePlayerState : ChasePlayerState
{
    private MediumShip _mediumShip;
    
    public MediumShipChasePlayerState(Enemy enemy, StateMachine stateMachine, MediumShip mediumShip) : base(enemy, stateMachine)
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
        
        if (isPlayerInsideAttackRange)
        {
            _stateMachine.ChangeState(_mediumShip.AttackState);
        }
        else if (isPlayerInsideViewRange == false)
        {
            _stateMachine.ChangeState(_mediumShip.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate(float fixedDeltaTime)
    {
        base.PhysicsUpdate(fixedDeltaTime);
    }
}
