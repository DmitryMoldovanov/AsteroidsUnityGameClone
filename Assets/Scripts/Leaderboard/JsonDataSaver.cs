using System;
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
    
    public void InitDataSaver()
    {
        _isInitialized = Load();
    }
    
    public void Save(ScoreResult data)
    {
        if (!_isInitialized)
        {
            InitDataSaver();
        }

        if (data.TotalKillCount > LastScoreResult.TotalKillCount)
        {
            _scoreResults.Add(data);
            _saveToFile = true;
            SaveToFile();
        }
    }

    public void SaveToFile()
    {
        if (_saveToFile)
        {
            ScoreResults scoreResults = new ScoreResults(_scoreResults.ToArray());
            var json = JsonUtility.ToJson(scoreResults);

            FileStream fileStream = new FileStream(_filePath, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }
    }

    public bool Load()
    {
        string json = String.Empty;
        
        if (File.Exists(_filePath))
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                json = reader.ReadToEnd();
            }
        }

        if (string.IsNullOrEmpty(json) || json.Equals("{}"))
        {
            _scoreResults.Add(new ScoreResult());
        }
        else
        {
            ScoreResults scoreResults = JsonUtility.FromJson<ScoreResults>(json);
            _scoreResults.AddRange(scoreResults.results);
        }

        LastScoreResult = _scoreResults[^1];
        
        return true;
    }

    public void DeleteScoreRecord(ScoreResult scoreResult)
    {
        _scoreResults.Remove(scoreResult);
        _saveToFile = true;
    }
}
