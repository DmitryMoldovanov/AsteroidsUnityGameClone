
using UnityEngine;

public class MediumShipPlayerDetectedState : PlayerDetectedState
{
    private MediumShip _mediumShip;
    
    public MediumShipPlayerDetectedState(Enemy enemy, StateMachine stateMachine, MediumShip mediumShip) : base(enemy, stateMachine)
    {
        _mediumShip = mediumShip;
    }

    public override void Enter()
    {
        base.Enter();

        _mediumShip.ImageSpawner.SpawnAlertImage(Color.yellow, _mediumShip.Transform);
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
            _stateMachine.ChangeState(_mediumShip.ChasePlayerState);
        }
        else if (isPlayerInsideAttackRange)
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

    protected override void DoChecks()
    {
        base.DoChecks();
    }
}
