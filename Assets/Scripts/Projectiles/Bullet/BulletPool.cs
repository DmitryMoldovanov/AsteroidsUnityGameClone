using UnityEngine;
using UnityEngine.Pool;

public class BulletPool
{
    private readonly ObjectPool<Bullet> _pool;

    private readonly Bullet _prefab;
    private readonly Transform _bulletParent;
    private readonly int _poolSize;

    public ObjectPool<Bullet> Pool => _pool;
    
    public BulletPool(Bullet prefab, Transform bulletParent, int poolSize)
    {
        _prefab = prefab;
        _bulletParent = bulletParent;
        _poolSize = poolSize;
        
        _pool = new UnityEngine.Pool.ObjectPool<Bullet>(CreateBullet,
            OnGetFromPool,
            OnReturnToPool,
            null,
            false,
            poolSize);
        
        CreateBullets();
    }

    private Bullet CreateBullet()
    {
        var bullet = Object.Instantiate(_prefab, _bulletParent.position, _bulletParent.rotation, _bulletParent);
        bullet.SetPool(_pool);
        bullet.Disable();
        return bullet;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.Enable();
    }

    private void OnReturnToPool(Bullet bullet)
    {
        bullet.Disable();
    }
    
    private void CreateBullets()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            var bullet =
                Object.Instantiate(_prefab, _bulletParent.position, _bulletParent.rotation, _bulletParent);
            bullet.SetPool(_pool);
            bullet.ReturnToPool(bullet);
        }
    }
}
