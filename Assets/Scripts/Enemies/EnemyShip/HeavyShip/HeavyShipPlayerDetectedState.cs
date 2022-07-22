using UnityEngine;

public class HeavyShipPlayerDetectedState : PlayerDetectedState
{
    private HeavyShip _heavyShip;
    
    public HeavyShipPlayerDetectedState(Enemy enemy, StateMachine stateMachine, HeavyShip heavyShip) : base(enemy, stateMachine)
    {
        _heavyShip = heavyShip;
    }

    public override void Enter()
    {
        base.Enter();
        
        _heavyShip.ImageSpawner.SpawnAlertImage(Color.yellow, _heavyShip.Transform);
        
        Debug.Log("HeavyShip entered PlayerDetectedState");
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
            _stateMachine.ChangeState(_heavyShip.ChasePlayerState);
        }
        else if (isPlayerInsideAttackRange)
        {
            _stateMachine.ChangeState(_heavyShip.AttackState);
        }
        else if (isPlayerInsideViewRange == false)
        {
            _stateMachine.ChangeState(_heavyShip.LookForPlayerState);
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
