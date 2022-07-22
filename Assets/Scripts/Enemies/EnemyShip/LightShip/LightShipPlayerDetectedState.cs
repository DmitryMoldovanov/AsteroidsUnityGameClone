using UnityEngine;

public class LightShipPlayerDetectedState : PlayerDetectedState
{
    private LightShip _lightShip;
    
    public LightShipPlayerDetectedState(Enemy enemy, StateMachine stateMachine, LightShip lightShip) : base(enemy, stateMachine)
    {
        _lightShip = lightShip;
    }

    public override void Enter()
    {
        base.Enter();
        
        _lightShip.ImageSpawner.SpawnAlertImage(Color.yellow, _lightShip.Transform);
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
            _stateMachine.ChangeState(_lightShip.ChasePlayerState);
        }
        else if (isPlayerInsideAttackRange)
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
