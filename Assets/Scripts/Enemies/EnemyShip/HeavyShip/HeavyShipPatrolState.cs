using UnityEngine;

public class HeavyShipPatrolState : PatrolState
{
    private HeavyShip _heavyShip;
    
    public HeavyShipPatrolState(Enemy enemy, StateMachine stateMachine, HeavyShip heavyShip) : base(enemy, stateMachine)
    {
        _heavyShip = heavyShip;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("HeavyShip entered PatrolState");
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
