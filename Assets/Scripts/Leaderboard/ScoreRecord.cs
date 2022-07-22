using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecord : MonoBehaviour
{
    public event Action<ScoreResult> OnDeleteButtonClickEvent; 

    [SerializeField] private TextMeshProUGUI _asteroidsKillCounter;
    [SerializeField] private TextMeshProUGUI _enemiesKillCounter;
    [SerializeField] private TextMeshProUGUI _date;
    [SerializeField] private Image _image;
    [SerializeField] private Button _deleteRecordButton;

    private ScoreResult _scoreResult;
    
    private void OnEnable()
    {
        _deleteRecordButton.onClick.AddListener(OnDeleteRecordButtonClicked);
    }

    private void OnDisable()
    {
        _deleteRecordButton.onClick.RemoveListener(OnDeleteRecordButtonClicked);
    }

    public void SetScore(Action<ScoreResult> deleteButtonEvent, ScoreResult scoreResult)
    {
        OnDeleteButtonClickEvent = deleteButtonEvent;
        _scoreResult = scoreResult;
        
        _asteroidsKillCounter.text = "A: " + scoreResult.AsteroidsKillCount;
        _enemiesKillCounter.text = "E: " + scoreResult.EnemiesKillCount;
        _date.text = scoreResult.DateTime;
    }

    public void SetScoreRecordColor(Color color)
    {
        _image.color = color;
    }

    private void OnDeleteRecordButtonClicked()
    {
        OnDeleteButtonClickEvent?.Invoke(_scoreResult);
        gameObject.SetActive(false);
    }
}
