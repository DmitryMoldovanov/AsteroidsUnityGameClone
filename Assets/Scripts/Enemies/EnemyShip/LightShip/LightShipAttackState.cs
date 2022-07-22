using UnityEngine;

public class LightShipAttackState : AttackState
{
    private LightShip _lightShip;
    
    public LightShipAttackState(Enemy enemy, StateMachine stateMachine, LightShip lightShip) : base(enemy, stateMachine)
    {
        _lightShip = lightShip;
    }

    public override void Enter()
    {
        base.Enter();
    
        _lightShip.ImageSpawner.SpawnAlertImage(Color.red, _lightShip.Transform);
    
        TriggerAttack();
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
            _stateMachine.ChangeState(_lightShip.ChasePlayerState);
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
        _lightShip.ToggleAttackState();
        _lightShip.Shoot();
    }

    protected override void FinishAttack()
    {
        base.FinishAttack();
        _lightShip.ToggleAttackState();
    }
}
