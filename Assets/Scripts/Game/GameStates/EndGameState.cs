using UnityEngine;

public class EndGameState : GameState
{
    [SerializeField] private EndGameView _endGameView;
    
    public override void InitGameState(GameContext gameContext)
    {
        base.InitGameState(gameContext);
        _endGameView.InitView(gameContext);
    }
    
    public override void Enter()
    {
        _gameContext.PauseGame();
        _gameContext.EnableUI(_endGameView.gameObject);
        _endGameView.SetCurrentScore(_gameContext.ScoreResult);
        _endGameView.SetBestScore(_gameContext.LastScoreResult);
    }

    public override void Exit()
    {
        _gameContext.DisableUI(_endGameView.gameObject);
        _gameContext.UnpauseGame();
    }

    public override GameStateName GetStateName()
    {
        return GameStateName.EndGame;
    }
}
