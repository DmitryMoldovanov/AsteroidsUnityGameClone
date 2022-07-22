using System;

[Serializable]
public class ScoreResult
{
    public int AsteroidsKillCount;
    public int EnemiesKillCount;
    public string DateTime;
    public int TotalKillCount;
    public bool IsDefault;
    
    public ScoreResult(int asteroidsKillCount, int enemiesKillCount)
    {
        AsteroidsKillCount = asteroidsKillCount;
        EnemiesKillCount = enemiesKillCount;
        DateTime = System.DateTime.Now.ToString();
        TotalKillCount = asteroidsKillCount + enemiesKillCount;
        IsDefault = false;
    }

    public ScoreResult()
    {
        AsteroidsKillCount = 0;
        EnemiesKillCount = 0;
        DateTime = System.DateTime.Now.ToString();

        IsDefault = true;
    }
}
