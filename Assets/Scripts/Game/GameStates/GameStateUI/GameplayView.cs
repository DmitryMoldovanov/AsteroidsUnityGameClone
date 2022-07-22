using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayView : View
{
    [SerializeField] private Button _pauseButton;

    [Header("Player UI")]
    [SerializeField] private Image _playerHealth;
    [SerializeField] private TextMeshProUGUI _asteroidKillScoreText;
    [SerializeField] private TextMeshProUGUI _enemyKillScoreText;

    private int _asteroidKillScore;
    private int _enemyKillScore;
    
    #region MONO

    public override void InitView(GameContext gameContext)
    {
        base.InitView(gameContext);
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
    }

    #endregion


    #region CALLBACKS

    private void OnPauseButtonClicked()
    {
        _gameContext.ChangeGameState(GameStateName.Paused);
    }

    public void IncreaseAsteroidKillScore()
    {
        _asteroidKillScore++;
        _asteroidKillScoreText.text = _asteroidKillScore.ToString();
    }

    public void IncreaseEnemyKillScore()
    {
        _enemyKillScore++;
        _enemyKillScoreText.text = _enemyKillScore.ToString();
    }

    public void DecreasePlayerHealth(int health)
    {
        var newHealth = Mathf.Clamp01(health * 0.01f);
        _playerHealth.fillAmount = newHealth;
    }

    public ScoreResult GetScoreResult()
    {
        return new ScoreResult(_asteroidKillScore, _enemyKillScore);
    }

    #endregion

}
