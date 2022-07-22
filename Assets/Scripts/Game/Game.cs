using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;

    [SerializeField] private GameSettings _gameSettings;

    [SerializeField] private GameContext _gameContext;
    [SerializeField] private GameplayView _gameplayView;

    private ObjPool<DamageText> _damageTextPool;
    private DamageTextComposite _damageTextComposite;
    
    private void Awake()
    {     
        _gameContext.Initialize();
        
        _damageTextPool = PoolLocator.Instance.GetPool<ObjPool<DamageText>>();

        _damageTextComposite = new DamageTextComposite(_damageTextPool);
        
        _player.Initialize(_playerData, OnPlayerDeath, _gameplayView, _damageTextComposite);
        _asteroidSpawner.Initialize(_gameplayView, _damageTextComposite);
        _enemySpawner.Initialize(_gameplayView, _damageTextComposite);

    }

    private void Start()
    {
        _gameContext.StartGame();
        
       _asteroidSpawner.StartSpawn();
       _enemySpawner.StartSpawn();
    }

    private void Update()
    {
        _player.LogicUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _player.PhysicsUpdate(Time.fixedDeltaTime);
    }

    private void OnPlayerDeath()
    {
        ScoreResult scoreResult = _gameplayView.GetScoreResult();
        _gameContext.SaveScoreResult(scoreResult);
        _gameContext.ChangeGameState(GameStateName.EndGame);
    }
}