using System.Threading.Tasks;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _startingBulletAmount;

    private ObjPool<Bullet> _bulletPool;

    private IProjectile _projectile => _bulletPool.Pool.Get();
    
    protected override void Initialize()
    {
        base.Initialize();
        
        _bulletPool = new ObjPool<Bullet>(_bulletPrefab, Transform.parent, _startingBulletAmount);
    }

    public async void Shoot()
    {
        while (CanAttack && !IsGamePaused)
        {
            _weapon.Fire(_projectile);
            await Task.Delay(_attackRate * 1000);
        }
    }
}