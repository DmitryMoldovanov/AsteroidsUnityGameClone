using UnityEngine;

public class GameInProgressState : GameState
{
    [SerializeField] private GameplayView _gameplayView;
    
    public override void InitGameState(GameContext gameContext)
    {
        base.InitGameState(gameContext);
        _gameplayView.InitView(gameContext);
    }

    public override void Enter()
    {
        _gameContext.UnpauseGame();
        _gameContext.EnableUI(_gameplayView.gameObject);
        
       // start Spawning Asteroids
       // start Spawning Enemies
    }

    public override void Exit()
    {
        _gameContext.DisableUI(_gameplayView.gameObject);
    }

    public override GameStateName GetStateName()
    {
        return GameStateName.GameInProgress;
    }
}
