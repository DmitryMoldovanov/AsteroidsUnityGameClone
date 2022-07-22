using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IUpdatable, IDamageable, IGameViewSubscribable
{
   public event Action OnDiedEvent;

   [SerializeField] private VariableJoystick _joystick;
   
   private IWeapon _weapon;
   private ObjPool<Bullet> _bulletPool;
   private HealthPoints _healthPoints;
   private IMovable _playerMovement;
   private GameplayView _gameplayView;
   private Rigidbody2D _rigidbody;
   
   public void Initialize(PlayerData playerData, Action OnPlayerDeath, GameplayView view, DamageTextComposite damageTextComposite)
   {
      _bulletPool = new ObjPool<Bullet>(playerData.BulletPrefab, transform, playerData.ProjectileAmount);
      
      _healthPoints = new HealthPoints(playerData.StartHealthPoints);
      _gameplayView = view;
      
      _rigidbody = GetComponent<Rigidbody2D>();
      _playerMovement = new PlayerMovement(_rigidbody, _joystick, transform, playerData.ThrustSpeed, playerData.TurnSpeed);
      
      SetWeapon(new MachineGun(transform, damageTextComposite));
      
      OnDiedEvent = OnPlayerDeath;
   }

   private void OnEnable()
   {
      _healthPoints.OnDied += OnPlayerDie;
      SubscribeToGameView(_gameplayView);
   }

   private void OnDisable()
   {
      _healthPoints.OnDied -= OnPlayerDie;
      UnSubscribeFromGameView(_gameplayView);
   }
   
   public void PhysicsUpdate(float fixedDeltaTime)
   {
      _playerMovement.Move(fixedDeltaTime);
   }

   public void LogicUpdate(float deltaTime)
   {
      
   }

   private void SetWeapon(IWeapon weapon)
   {
      _weapon = weapon;
   }

   public void Shoot()
   {
      _weapon.Fire(_bulletPool.Pool.Get());
   }

   private void OnPlayerDie()
   {
      gameObject.SetActive(false);
      OnDiedEvent?.Invoke();
   }

   public void TakeDamage(int damage)
   {
      _healthPoints.TakeDamage(damage);
   }

   public void SubscribeToGameView(GameplayView view)
   {
      _healthPoints.OnChanged += view.DecreasePlayerHealth;
   }

   public void UnSubscribeFromGameView(GameplayView view)
   {
      _healthPoints.OnChanged -= view.DecreasePlayerHealth;
   }
}
