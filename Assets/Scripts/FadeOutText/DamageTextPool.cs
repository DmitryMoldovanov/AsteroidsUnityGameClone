using UnityEngine;
using UnityEngine.Pool;

public class DamageTextPool
{
    private readonly ObjectPool<DamageText> _pool;
    private readonly DamageText _prefab;
    private Transform _parent;
    
    public ObjectPool<DamageText> Pool => _pool;
    
    public DamageTextPool(DamageText prefab, int defaultPoolCapacity, Transform poolParent)
    {
        _prefab = prefab;
        _parent = poolParent;
        
        _pool = new ObjectPool<DamageText>(CreateDamageText,
            OnGetFromPool,
            OnReturnToPool,
            null,
            false,
            defaultPoolCapacity);

        CreateDamageTexts(defaultPoolCapacity);
    }

    private void CreateDamageTexts(int defaultPoolCapacity)
    {
        for (int i = 0; i < defaultPoolCapacity; i++)
        {
            CreateDamageText();
        }
    }

    private DamageText CreateDamageText()
    {
        var damageText = GameObject.Instantiate(_prefab, _parent);
        damageText.SetPool(_pool);
        damageText.ReturnToPool(damageText);
        return damageText;
    }

    private void OnGetFromPool(DamageText damageText)
    {
        damageText.Enable();
    }

    private void OnReturnToPool(DamageText damageText)
    {
        damageText.Disable();
    }
}
