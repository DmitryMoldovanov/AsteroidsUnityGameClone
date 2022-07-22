using UnityEngine;

public class PauseState : GameState
{
    [SerializeField] private PauseMenuView _pauseMenuView;
    
    public override void InitGameState(GameContext gameContext)
    {
        base.InitGameState(gameContext);
        _pauseMenuView.InitView(gameContext);
    }

    public override void Enter()
    {
        _gameContext.PauseGame();
        _gameContext.EnableUI(_pauseMenuView.gameObject);
    }

    public override void Exit()
    {
        _gameContext.DisableUI(_pauseMenuView.gameObject);
        _gameContext.UnpauseGame();
    }
    
    public override GameStateName GetStateName()
    {
        return GameStateName.Paused;
    }
}
