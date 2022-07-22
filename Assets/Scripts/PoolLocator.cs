using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolLocator : Singleton<PoolLocator>
{
    [Header("DamageText")]
    [SerializeField] private DamageText _damageTextPrefab;
    [SerializeField] private int _damageTextDefaultAmount;
    [SerializeField] private Transform _damageTextPoolParent;

    private IDictionary<Type, object> _pools;

    private void Awake()
    {
        Initialize();
        RegistryPools();
    }

    private void RegistryPools()
    {
        _pools = new Dictionary<Type, object>();

        _pools.Add(typeof(ObjPool<DamageText>), new ObjPool<DamageText>(_damageTextPrefab, _damageTextPoolParent, _damageTextDefaultAmount));
    }

    public T GetPool<T>()
    {
        try
        {
            return (T) _pools[typeof(T)];
        }
        catch
        {
            throw new ApplicationException("The requested Pool is not found");
        }
    }
}
