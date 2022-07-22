using UnityEngine;

public interface IProjectile
{
    public void Initialize(Transform parent, DamageTextComposite damageTextComposite);
    public void Shoot();
}
