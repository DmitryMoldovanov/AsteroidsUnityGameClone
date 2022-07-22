using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected GameContext _gameContext;

    public virtual void InitView(GameContext gameContext)
    {
        _gameContext = gameContext;
    }


}
