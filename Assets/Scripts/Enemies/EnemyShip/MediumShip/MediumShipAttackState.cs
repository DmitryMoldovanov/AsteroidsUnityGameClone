
using UnityEngine;

public class MediumShipAttackState : AttackState
{
    private MediumShip _mediumShip;
    
    public MediumShipAttackState(Enemy enemy, StateMachine stateMachine, MediumShip mediumShip) : base(enemy, stateMachine)
    {
        _mediumShip = mediumShip;
    }

    public override void Enter()
    {
        base.Enter();

        _mediumShip.ImageSpawner.SpawnAlertImage(Color.red, _mediumShip.Transform);
    
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
    
    protected override void TriggerAttack()
    {
        base.TriggerAttack();
        _mediumShip.ToggleAttackState();
        _mediumShip.Attack();
    }

    protected override void FinishAttack()
    {
        base.FinishAttack();
        _mediumShip.ToggleAttackState();
    }
}
