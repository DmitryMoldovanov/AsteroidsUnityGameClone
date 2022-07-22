
public class StateMachine
{
    public EntityState CurrentEntityState { get; private set; }

    public void Initialize(EntityState startingEntityState)
    {
        CurrentEntityState = startingEntityState;
        CurrentEntityState.Enter();
    }

    public void ChangeState(EntityState newEntityState)
    {
        CurrentEntityState.Exit();
        CurrentEntityState = newEntityState;
        CurrentEntityState.Enter();
    }
}
