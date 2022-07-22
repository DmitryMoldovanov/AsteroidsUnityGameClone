using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _projectileAmount;
    [SerializeField] private int _startHealthPoints;
    [SerializeField] private float _playerThrustSpeed;
    [SerializeField] private float _playerTurnSpeed;

    public Bullet BulletPrefab => _prefab;
    public int ProjectileAmount => _projectileAmount;
    public int StartHealthPoints => _startHealthPoints;
    public float ThrustSpeed => _playerThrustSpeed;
    public float TurnSpeed => _playerTurnSpeed;
}
