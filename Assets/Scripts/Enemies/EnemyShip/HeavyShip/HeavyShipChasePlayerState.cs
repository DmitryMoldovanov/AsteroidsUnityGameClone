using UnityEngine;

public class HeavyShipChasePlayerState : ChasePlayerState
{
    private HeavyShip _heavyShip;
    
    public HeavyShipChasePlayerState(Enemy enemy, StateMachine stateMachine, HeavyShip heavyShip) : base(enemy, stateMachine)
    {
        _heavyShip = heavyShip;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("HeavyShip entered FollowPlayerState");
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
