
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private PlayerData _playerData;
    
    public PlayerData PlayerData => _playerData;
}
