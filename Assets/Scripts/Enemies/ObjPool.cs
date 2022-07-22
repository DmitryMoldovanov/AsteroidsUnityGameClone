using UnityEngine;
using UnityEngine.Pool;

public class ObjPool<T> where T : PooledObject<T>
{
    private readonly ObjectPool<T> _pool;
    private readonly T _prefab;
    private readonly Transform _poolParent;
    
    public ObjectPool<T> Pool => _pool;
    
    public ObjPool(T prefab, Transform poolParent, int defaultPoolCapacity)
    {
        _prefab = prefab;
        _poolParent = poolParent;
        
        _pool = new ObjectPool<T>(CreateObject,
            OnGetFromPool,
            OnReturnToPool,
            null,
            false,
            defaultPoolCapacity);
        
        PreCreateObjects(defaultPoolCapacity);
    }

    private void PreCreateObjects(int defaultPoolCapacity)
    {
        for (int i = 0; i < defaultPoolCapacity; i++)
        {
            CreateObject();
        }
    }
    
    private T CreateObject()
    {
        var obj = Object.Instantiate(_prefab, _poolParent);
        obj.SetPool(_pool);
        obj.ReturnToPool(obj);
        return obj;
    }
    
    private void OnGetFromPool(T obj)
    {
        obj.Enable();
    }

    private void OnReturnToPool(T obj)
    {
        obj.Disable();
    }
}
