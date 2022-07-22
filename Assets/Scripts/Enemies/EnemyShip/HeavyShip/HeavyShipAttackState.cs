using UnityEngine;

public class HeavyShipAttackState : AttackState
{
    private HeavyShip _heavyShip;
    
    public HeavyShipAttackState(Enemy enemy, StateMachine stateMachine, HeavyShip heavyShip) : base(enemy, stateMachine)
    {
        _heavyShip = heavyShip;
    }

    public override void Enter()
    {
        base.Enter();
        
        _heavyShip.ImageSpawner.SpawnAlertImage(Color.red, _heavyShip.Transform);
        
        TriggerAttack();
        Debug.Log("HeavyShip entered AttackState");
    }

    public override void Exit()
    {
        base.Exit();
        FinishAttack();
    }

    public override void LogicUpdate(float deltaTime)
    {
        base.LogicUpdate(deltaTime);

        if (isPlayerInsideViewRange == false)
        {
            _stateMachine.ChangeState(_heavyShip.ChasePlayerState);
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

    protected override void TriggerAttack()
    {
        base.TriggerAttack();
        _heavyShip.ToggleAttackState();
        _heavyShip.Shoot();
    }

    protected override void FinishAttack()
    {
        base.FinishAttack();
        _heavyShip.ToggleAttackState();
    }
}
