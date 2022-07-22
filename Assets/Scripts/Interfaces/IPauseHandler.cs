
public interface IPauseHandler
{
    public bool IsGamePaused { get; }
    public void SetPause(bool isPaused);
}
