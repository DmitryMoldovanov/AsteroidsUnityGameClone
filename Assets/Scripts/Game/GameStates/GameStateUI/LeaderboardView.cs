using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : View
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private ScoreRecord _scoreRecordPrefab;
    [SerializeField] private Color _lastBestScoreColor;
    [SerializeField] private GameObject _scoreRecordsParent;

    private JsonDataSaver _dataSaver;
    private LevelManager _levelManager;
    
    #region MONO

    private void Awake()
    {
        _dataSaver = new JsonDataSaver();
        _levelManager = LevelManager.Instance;
    }

    private void Start()
    {
        _dataSaver.InitDataSaver();
        InitScoreRecords(_dataSaver.ScoreResultsArray);
    }

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
    }

    #endregion
    
    private async void OnMainMenuButtonClicked()
    {
        _dataSaver.SaveToFile();
        
        gameObject.SetActive(false);
        await _levelManager.LoadSceneAsync(SceneNames.MainMenu.ToString());
    }

    private void InitScoreRecords(ScoreResult[] scoreResults)
    {
        foreach (var scoreResult in scoreResults.Reverse())
        {
            ScoreRecord scoreRecord = Instantiate(_scoreRecordPrefab, _scoreRecordsParent.transform);
            scoreRecord.SetScore(_dataSaver.DeleteScoreRecord, scoreResult);
            
            if (scoreResult.Equals(scoreResults.Last()))
            {
                scoreRecord.SetScoreRecordColor(_lastBestScoreColor);
            }
        }
    }
    
}
