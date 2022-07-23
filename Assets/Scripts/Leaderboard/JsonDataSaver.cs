using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JsonDataSaver : IDataSaver
{
    private readonly string _saveFileName = "/ScoreResultsSave.json";
    private readonly string _filePath;
    
    private List<ScoreResult> _scoreResults = new ();

    private bool _isInitialized;

    public ScoreResult LastScoreResult { get; private set; }

    public ScoreResult[] ScoreResultsArray => _scoreResults.ToArray();

    private bool _saveToFile;
    
    public JsonDataSaver()
    {
        _filePath = Application.persistentDataPath + _saveFileName;
    }
    
    public async void InitDataSaver()
    {
        _isInitialized = await LoadAsync();
    }
    
    public async void Save(ScoreResult data)
    {
        if (!_isInitialized)
        {
            InitDataSaver();
        }

        if (data.TotalKillCount > LastScoreResult.TotalKillCount)
        {
            _scoreResults.Add(data);
            _saveToFile = true;
            await SaveCacheToFileAsync();
        }
    }

    public async Task SaveCacheToFileAsync()
    {
        if (_saveToFile)
        {
            var fileStream = new FileStream(_filePath, FileMode.Create);

            await using var writer = new StreamWriter(fileStream);
            foreach (var scoreResult in ScoreResultsArray)
            {
                var jsonToSave = JsonUtility.ToJson(scoreResult);
                await writer.WriteLineAsync(jsonToSave);
            }
        }
    }

    public async Task<bool> LoadAsync()
    {
        return await CacheScoreResultsAsync();
    }

    private async IAsyncEnumerable<string> LoadDataFromFileAsync()
    {
        if (File.Exists(_filePath))
        {
            using var reader = new StreamReader(_filePath);
            string json;
            while ((json = await reader.ReadLineAsync()) != null)
            {
                yield return json;
            }
        }
        else
        {
            LastScoreResult = new ScoreResult();
        }
    }

    private async Task<bool> CacheScoreResultsAsync()
    {
        await foreach (var saveResult in LoadDataFromFileAsync())
        {
            if (IsJsonEmpty(saveResult))
            {
                _scoreResults.Add(new ScoreResult());
            }
            else
            {
                var scoreResult = JsonUtility.FromJson<ScoreResult>(saveResult);
                _scoreResults.Add(scoreResult);
            }

            LastScoreResult = _scoreResults[^1];
        }

        return true;
    }

    public async IAsyncEnumerable<ScoreResult> GetScoreResultsAsync()
    {
        await foreach (var saveResult in LoadDataFromFileAsync())
        {
            ScoreResult scoreResult;
            if (IsJsonEmpty(saveResult))
            {
                scoreResult = new ScoreResult();
                _scoreResults.Add(scoreResult);
                yield return scoreResult;
            }
            else
            {
                scoreResult = JsonUtility.FromJson<ScoreResult>(saveResult);
                _scoreResults.Add(scoreResult);
                yield return scoreResult;
            }
        }
    }

    public void DeleteScoreRecord(ScoreResult scoreResult)
    {
        _scoreResults.Remove(scoreResult);
        _saveToFile = true;
    }

    private bool IsJsonEmpty(string scoreResult)
    {
        return string.IsNullOrEmpty(scoreResult) || scoreResult.Equals("{}");
    }
}
