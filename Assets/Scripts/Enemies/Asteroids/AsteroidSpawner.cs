using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour, IPauseHandler
{
    [SerializeField] private Asteroid _asteroidPrefab;
    [SerializeField] private BoxCollider2D _gameAreaBoundary;
    [SerializeField] private int _bufferAmount;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnDistance = 10f;

    private ObjPool<Asteroid> _asteroidPool;
    private Transform _transform;
    private GameplayView _gameplayView;
    private DamageTextComposite _damageTextComposite;
    
    public bool IsGamePaused { get; private set; }

    #region MONO

    public void Initialize(GameplayView view, DamageTextComposite damageTextComposite)
    {
        _transform = transform;
        _asteroidPool = new ObjPool<Asteroid>(_asteroidPrefab, _transform, _bufferAmount);
        _gameplayView = view;
        _damageTextComposite = damageTextComposite;
    }
    
    private void OnEnable()
    {
        GameContext.Instance.PauseManager.Register(this);
    }

    private void OnDisable()
    {
        GameContext.Instance.PauseManager.UnRegister(this);
    }

    #endregion
    
    public void StartSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }
    
    public void SetPause(bool isPaused)
    {
        IsGamePaused = isPaused;
    }

    private IEnumerator SpawnCoroutine()
    {
        var spawnDelay = new WaitForSeconds(_spawnRate);
        
        while (!IsGamePaused)
        {
            Spawn();
            yield return spawnDelay;
        }
    }
    
    private void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
        Vector3 spawnPosition = _transform.position + spawnDirection;

        var bounds = _gameAreaBoundary.bounds;
        float variance = Random.Range(-bounds.extents.x, bounds.extents.x);
        var rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid asteroid = _asteroidPool.Pool.Get();
        asteroid.SubscribeToGameView(_gameplayView);
        asteroid.SetDependencies(_damageTextComposite);
        asteroid.SetTransform(spawnPosition, rotation);
        asteroid.SetTrajectory(rotation * -spawnDirection);
    }
}
