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

    private IDataSaver _dataSaver;
    private LevelManager _levelManager;
    
    #region MONO

    private void Awake()
    {
        _dataSaver = new JsonDataSaver();
        _levelManager = LevelManager.Instance;
    }

    private void Start()
    {
        //_dataSaver.InitDataSaver();
        InitScoreRecords();
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
        await _dataSaver.SaveCacheToFileAsync();
        
        gameObject.SetActive(false);
        await _levelManager.LoadSceneAsync(SceneNames.MainMenu.ToString());
    }

    private async void InitScoreRecords()
    {
        await foreach (var scoreResult in _dataSaver.GetScoreResultsAsync())
        {
            ScoreRecord scoreRecord = Instantiate(_scoreRecordPrefab, _scoreRecordsParent.transform);
            scoreRecord.transform.SetAsFirstSibling();
            scoreRecord.SetScore(_dataSaver.DeleteScoreRecord, scoreResult);
        }

        if (_scoreRecordsParent.transform.childCount > 0)
        {
            _scoreRecordsParent.transform.GetChild(0).TryGetComponent(out ScoreRecord firstScoreRecord);
            firstScoreRecord.SetScoreRecordColor(_lastBestScoreColor);
        }
    }
    
}
