using UnityEngine;

public class MachineGun: IWeapon
{
    private Transform _projectileParent;
    private DamageTextComposite _damageTextComposite;

    public MachineGun(Transform projectileParent, DamageTextComposite damageTextComposite)
    {
        _projectileParent = projectileParent;
        _damageTextComposite = damageTextComposite;
    }
    
    public void Fire(IProjectile projectile)
    {
        projectile.Initialize(_projectileParent, _damageTextComposite);
        projectile.Shoot();
    }
}
