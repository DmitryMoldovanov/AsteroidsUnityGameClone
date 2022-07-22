using UnityEngine;
using UnityEngine.Pool;

public class AsteroidPool
{
    private readonly ObjectPool<Asteroid> _pool;
    private readonly Asteroid _prefab;

    private GameplayView _gameplayView;
    
    public ObjectPool<Asteroid> Pool => _pool;
    
    public AsteroidPool(Asteroid prefab, int defaultPoolCapacity, GameplayView view)
    {
        _prefab = prefab;
        _gameplayView = view;
        
        _pool = new ObjectPool<Asteroid>(CreateAsteroid,
            OnGetFromPool,
            OnReturnToPool,
            OnDestroy,
            false,
            defaultPoolCapacity);

        CreateAsteroids(defaultPoolCapacity);
    }

    private void CreateAsteroids(int defaultPoolCapacity)
    {
        for (int i = 0; i < defaultPoolCapacity; i++)
        {
            Asteroid asteroid = CreateAsteroid();
        }
    }

    private Asteroid CreateAsteroid()
    {
        var asteroid = GameObject.Instantiate(_prefab);
        asteroid.SubscribeToGameView(_gameplayView);
        asteroid.SetPool(_pool);
        asteroid.ReturnToPool(asteroid);
        return asteroid;
    }

    private void OnGetFromPool(Asteroid asteroid)
    {
        asteroid.Enable();
    }

    private void OnReturnToPool(Asteroid asteroid)
    {
        asteroid.Disable();
        asteroid.MoveToNextGeneration();
    }

    private void OnDestroy(Asteroid asteroid)
    {
        asteroid.UnSubscribeFromGameView(_gameplayView);
    }
}
