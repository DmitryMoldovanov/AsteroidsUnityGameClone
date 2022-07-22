using System.Threading.Tasks;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] [Range(0, 2)] private int _chargeDelay;
    [SerializeField] [Range(100, 200)] private float _chargeForce;
    
    protected override void Initialize()
    {
        base.Initialize();
    }
    
    public async void Attack()
    {
        while (CanAttack && !IsGamePaused)
        {
            ImageSpawner.SpawnReadyToAttackImage(Color.green, Transform);
            await Task.Delay(_chargeDelay * 1000);
            Charge();
            await Task.Delay(_attackRate * 1000);
        }
    }

    private void Charge()
    {
        _rigidbody.AddForce((Collider.GetTargetPosition() - Transform.position).normalized * _chargeForce);
    }
}
