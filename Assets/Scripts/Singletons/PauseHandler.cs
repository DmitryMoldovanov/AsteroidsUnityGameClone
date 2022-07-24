using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : IPauseHandler
{
    private readonly List<IPauseHandler> _pauseHandlers = new();

    public bool IsGamePaused { get; private set; }

    public void Register(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Add(pauseHandler);
    }
    
    public void UnRegister(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Remove(pauseHandler);
    }
   

    public void SetPause(bool isPaused)
    {
        IsGamePaused = isPaused;
        
        var timeScale = isPaused ? 0f : 1f;
        Time.timeScale = timeScale;
        
        foreach (var handler in _pauseHandlers)
        {
            handler.SetPause(isPaused);
        }
    }
}
