using UnityEngine;

public abstract class GameState : MonoBehaviour, IState
{
    protected GameContext _gameContext;

    public virtual void InitGameState(GameContext gameContext)
    {
        _gameContext = gameContext;
    }
    
    public abstract void Enter();

    public abstract void Exit();

    public abstract GameStateName GetStateName();
}
