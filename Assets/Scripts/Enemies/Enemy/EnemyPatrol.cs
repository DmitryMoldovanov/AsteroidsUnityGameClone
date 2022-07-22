
using UnityEngine;

public class EnemyPatrol : IPatrolable
{
    private Transform _transform;
    private float _turnSpeed;
    
    public EnemyPatrol(Transform transform, float turnSpeed)
    {
        _transform = transform;
        _turnSpeed = turnSpeed;
    }
    
    public Vector2 CalculatePatrolPoint()
    {
        return (Vector2)_transform.position + Random.insideUnitCircle;
    }

    public void Patrol(Vector3 pointToPatrolAround, float fixedDeltaTime)
    {
        //_transform.RotateAround(pointToPatrolAround, Vector3.forward, _turnSpeed * fixedDeltaTime);
        _transform.Rotate(Vector3.forward, _turnSpeed * fixedDeltaTime);
    }
}
