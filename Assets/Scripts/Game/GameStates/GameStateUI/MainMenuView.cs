using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _leaderboardButton;

    private LevelManager _levelManager;
    
    #region MONO

    private void Awake()
    {
        _levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClicked);
    }

    #endregion

    #region CALLBACKS

    private async void OnStartGameButtonClicked()
    {
        gameObject.SetActive(false);
        await _levelManager.LoadSceneAsync(SceneNames.GameplayMenu.ToString());
    }

    private async void OnLeaderboardButtonClicked()
    {
        gameObject.SetActive(false);
        await _levelManager.LoadSceneAsync(SceneNames.LeaderboardMenu.ToString());
    }

    #endregion
    
}
