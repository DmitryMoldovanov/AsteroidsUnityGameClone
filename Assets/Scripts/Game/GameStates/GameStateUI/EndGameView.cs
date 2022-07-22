using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : View
{
    [SerializeField] private TextMeshProUGUI _asteroidKillScoreText;
    [SerializeField] private TextMeshProUGUI _enemyKillScoreText;
    
    [SerializeField] private TextMeshProUGUI _asteroidBestKillScoreText;
    [SerializeField] private TextMeshProUGUI _enemyBestKillScoreText;
    
    [SerializeField] private Button _replayGameButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _leaderboardButton;
    
    private LevelManager _levelManager;
    
    #region MONO

    public override void InitView(GameContext gameContext)
    {
        base.InitView(gameContext);

        _levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
        _replayGameButton.onClick.AddListener(OnReplayButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
    }

    private void OnDisable()
    {
        _replayGameButton.onClick.RemoveListener(OnReplayButtonClicked);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClicked);
    }

    #endregion

    #region CALLBACKS
    
    private async void OnReplayButtonClicked()
    {
        _gameContext.DisableUI(gameObject);
        await _levelManager.LoadSceneAsync(SceneNames.GameplayMenu.ToString());
    }

    private async void OnMainMenuButtonClicked()
    {
        _gameContext.DisableUI(gameObject);
        await _levelManager.LoadSceneAsync(SceneNames.MainMenu.ToString());
    }

    private async void OnLeaderboardButtonClicked()
    {
        _gameContext.DisableUI(gameObject);
        await _levelManager.LoadSceneAsync(SceneNames.LeaderboardMenu.ToString());
    }

    public void SetCurrentScore(ScoreResult scoreResult)
    {
        _asteroidKillScoreText.text = scoreResult.AsteroidsKillCount.ToString();
        _enemyKillScoreText.text = scoreResult.EnemiesKillCount.ToString();
    }

    public void SetBestScore(ScoreResult scoreResult)
    {
        if (scoreResult.IsDefault)
        {
            _asteroidBestKillScoreText.text = "-null-";
            _enemyBestKillScoreText.text = "-null-";
        }
        else
        {
            _asteroidBestKillScoreText.text = scoreResult.AsteroidsKillCount.ToString();
            _enemyBestKillScoreText.text = scoreResult.EnemiesKillCount.ToString();
        }
    }
    
    #endregion
}
