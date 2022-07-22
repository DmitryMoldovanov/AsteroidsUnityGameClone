using UnityEngine;

public class HeavyShipIdleState : IdleState
{
    private HeavyShip _heavyShip;
    
    public HeavyShipIdleState(Enemy enemy, StateMachine stateMachine, HeavyShip heavyShip) : base(enemy, stateMachine)
    {
        _heavyShip = heavyShip;
    }

    protected override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("HeavyShip entered IdleState");
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
            _stateMachine.ChangeState(_heavyShip.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            _stateMachine.ChangeState(_heavyShip.PatrolState);
        }
    }

    public override void PhysicsUpdate(float fixedDeltaTime)
    {
        base.PhysicsUpdate(fixedDeltaTime);
    }
}
