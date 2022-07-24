using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : View
{
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _leaderboardButton;

    private LevelLoader _levelLoader;
    
    #region MONO

    public override void InitView(GameContext gameContext)
    {
        base.InitView(gameContext);

        _levelLoader = LevelLoader.Instance;
    }

    private void OnEnable()
    {
        _resumeGameButton.onClick.AddListener(OnResumeButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
    }

    private void OnDisable()
    {
        _resumeGameButton.onClick.RemoveListener(OnResumeButtonClicked);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClicked);
    }

    #endregion

    #region CALLBACKS
    
    private void OnResumeButtonClicked()
    {
        _gameContext.ChangeGameState(GameStateName.GameInProgress);
    }

    private async void OnMainMenuButtonClicked()
    {
        _gameContext.DisableUI(gameObject);
        await _levelLoader.LoadSceneAsync(SceneNames.MainMenu.ToString());
    }

    private void OnLeaderboardButtonClicked()
    {
        
    }
    
    #endregion
}
