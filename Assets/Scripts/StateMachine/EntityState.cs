
public abstract class EntityState : IState, IUpdatable
{
    protected StateMachine _stateMachine;

    public abstract void Enter();

    public abstract void Exit();

    public abstract void LogicUpdate(float deltaTime);

    public abstract void PhysicsUpdate(float fixedDeltaTime);

    protected abstract void DoChecks();
}
