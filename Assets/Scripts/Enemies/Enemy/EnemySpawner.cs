using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IPauseHandler
{
    [SerializeField] private HeavyShip _heavyShipPrefab;
    [SerializeField] private int _heavyShipSpawnProbability;
    
    [SerializeField] private MediumShip _mediumShipPrefab;
    [SerializeField] private int _mediumShipSpawnProbability;
    
    [SerializeField] private LightShip _lightShipPrefab;
    [SerializeField] private int _lightShipSpawnProbability;
    
    [SerializeField] private BoxCollider2D _gameAreaBoundary;
    [SerializeField] private int _bufferAmount;
    [SerializeField] private float _spawnRate;

    private ObjPool<Enemy> _heavyShipPool;
    private ObjPool<Enemy> _mediumShipPool;
    private ObjPool<Enemy> _lightShipPool;
    private GameplayView _gameplayView;
    private DamageTextComposite _damageTextComposite;
    
    public bool IsGamePaused { get; private set; }

    #region MONO

    public void Initialize(GameplayView view, DamageTextComposite damageTextComposite)
    {
        _heavyShipPool = new ObjPool<Enemy>(_heavyShipPrefab, transform, _bufferAmount);
        _mediumShipPool = new ObjPool<Enemy>(_mediumShipPrefab, transform, _bufferAmount);
        _lightShipPool = new ObjPool<Enemy>(_lightShipPrefab, transform, _bufferAmount);
        
        _gameplayView = view;
        _damageTextComposite = damageTextComposite;
    }
    
    private void OnEnable()
    {
        GameContext.Instance.PauseHandler.Register(this);
    }

    private void OnDisable()
    {
        GameContext.Instance.PauseHandler.UnRegister(this);
    }

    #endregion
    
    public void StartSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        var spawnDelay = new WaitForSeconds(_spawnRate);
        yield return spawnDelay;
        
        while (!IsGamePaused)
        {
            Spawn();
            yield return spawnDelay;
        }
    }
    
    private void Spawn()
    {
        Enemy enemy;

        int rand = Random.Range(0, 100);

        if (rand <= _lightShipSpawnProbability)
        {
            enemy = _lightShipPool.Pool.Get();
        }
        else if (rand > _lightShipSpawnProbability && rand <= _mediumShipSpawnProbability + _lightShipSpawnProbability)
        {
            enemy = _mediumShipPool.Pool.Get();
        }
        else
        {
            enemy = _heavyShipPool.Pool.Get();
        }
        
        enemy.SubscribeToGameView(_gameplayView);
        enemy.SetDependencies(_damageTextComposite);
        enemy.PrepareToSpawn(CalculateSpawnPosition());
    }

    private Vector2 CalculateSpawnPosition()
    {
        var bounds = _gameAreaBoundary.bounds;
        float horizontalVariance = Random.Range(-bounds.extents.x, bounds.extents.x);
        float verticalVariance = Random.Range(-bounds.extents.y, bounds.extents.y);
        Vector2 spawnPosition = new Vector2(horizontalVariance, verticalVariance);

        return spawnPosition;
    }

    public void SetPause(bool isPaused)
    {
        IsGamePaused = isPaused;
    }
}
