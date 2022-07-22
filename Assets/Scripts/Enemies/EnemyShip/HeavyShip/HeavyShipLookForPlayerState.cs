using UnityEngine;

public class HeavyShipLookForPlayerState : LookForPlayerState
{
    private HeavyShip _heavyShip;
    public HeavyShipLookForPlayerState(Enemy enemy, StateMachine stateMachine, HeavyShip heavyShip) : base(enemy, stateMachine)
    {
        _heavyShip = heavyShip;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("HeavyShip entered LookForPlayerState");
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
        else
        {
            _stateMachine.ChangeState(_heavyShip.PatrolState);
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
