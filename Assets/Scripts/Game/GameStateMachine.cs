
public class GameStateMachine<T> where T : IState
{
    private T _currentState;

    public void InitializeFirstState(T state){
        _currentState = state;
        _currentState.Enter();
    }

    public void ChangeState(T newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
