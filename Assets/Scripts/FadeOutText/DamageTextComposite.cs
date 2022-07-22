using UnityEngine;

public class DamageTextComposite
{
    private ObjPool<DamageText> _damageTextPool;
    
    public DamageTextComposite(ObjPool<DamageText> damageTextPool)
    {
        _damageTextPool = damageTextPool;
    }
    
    public void ShowDamageText(int damage, bool isCritical, Vector3 position)
    {
        DamageText damageText = _damageTextPool.Pool.Get();
        damageText.SetText(damage.ToString());
        damageText.ShowText(isCritical, position);
    }
}
