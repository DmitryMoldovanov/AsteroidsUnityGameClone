using UnityEngine;
using UnityEngine.Pool;

public class AsteroidFracturer : IFracturable
{
    private Transform _transform;
    private readonly ObjectPool<Asteroid> _asteroidPool;

    private readonly float _size;
    private readonly float _speed;
    private readonly float _fractureSizeBoundary;
    private readonly float _minSize;
    private readonly GameplayView _gameplayView;
    private readonly DamageTextComposite _damageTextComposite;
    
    public AsteroidFracturer(ObjectPool<Asteroid> pool,
        Transform asteroidTransform,
        float asteroidSize,
        float asteroidSpeed,
        float fractureSizeBoundary,
        float minSize,
        GameplayView gameplayView,
        DamageTextComposite damageTextComposite)
    {
        _asteroidPool = pool;
        _transform = asteroidTransform;
        _size = asteroidSize;
        _speed = asteroidSpeed;
        _fractureSizeBoundary = fractureSizeBoundary;
        _minSize = minSize;
        _gameplayView = gameplayView;
        _damageTextComposite = damageTextComposite;
    }

    public void Fracture(int amountOfNewPeaces)
    {
        if (_size * _fractureSizeBoundary >= _minSize)
        {
            Vector2 position = _transform.position;
            position += Random.insideUnitCircle * _fractureSizeBoundary;
            float newSize = _size * _fractureSizeBoundary;
            
            for (int i = 0; i < amountOfNewPeaces; i++)
            {
                Debug.Log(_asteroidPool);
                Asteroid asteroid = _asteroidPool.Get();
                
                asteroid.SubscribeToGameView(_gameplayView);
                asteroid.SetDependencies(_damageTextComposite);
                asteroid.PrepareToFracture(position, _transform.rotation, newSize);
                asteroid.SetTrajectory(Random.insideUnitSphere.normalized * _speed);
            }
        }
    }
}
