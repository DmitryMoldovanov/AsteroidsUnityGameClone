using UnityEngine;
using UnityEngine.Pool;

public abstract class PooledObject<T> : MonoBehaviour where T : class
{
    private ObjectPool<T> _pool;

    protected ObjectPool<T> Pool => _pool;
    
    public void SetPool(ObjectPool<T> pool) => _pool = pool;

    public void ReturnToPool(T obj) => _pool.Release(obj);

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    protected abstract void ResetObject();
}
