using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDataSaver
{
    ScoreResult LastScoreResult { get; }
    
    void InitDataSaver();
    void Save(ScoreResult dataToSave);
    Task SaveCacheToFileAsync();
    Task<bool> LoadAsync();
    IAsyncEnumerable<ScoreResult> GetScoreResultsAsync();
    void DeleteScoreRecord(ScoreResult scoreResult);
}
