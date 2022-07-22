using System;

[Serializable]
public class ScoreResults
{
    public ScoreResult[] results;

    public ScoreResults(ScoreResult[] scoreResults)
    {
        results = scoreResults;
    }
}
