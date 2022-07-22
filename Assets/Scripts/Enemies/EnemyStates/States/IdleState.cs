using UnityEngine;

public class IdleState : EnemyState
{
    
    protected bool isIdleTimeOver;

    protected float timeToIdle;
    
    public IdleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        isIdleTimeOver = false;
        SetTimeToIdle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(float deltaTime)
    {
        base.LogicUpdate(deltaTime);
        
        if (Time.time >= stateEntryTime + timeToIdle)
        {
            isIdleTimeOver = true;
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
    
    private void SetTimeToIdle()
    {
        timeToIdle = Random.Range(1f, 3f);
    }
}
