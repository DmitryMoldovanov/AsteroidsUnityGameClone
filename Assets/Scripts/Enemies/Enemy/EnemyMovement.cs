using UnityEngine;

public class EnemyMovement : IMovable
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Vector3 _directionToTarget;
    private float _moveSpeed;
    
    public EnemyMovement(Transform transform, Rigidbody2D rigidbody, float moveSpeed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _moveSpeed = moveSpeed;
    }
    
    public void Move(float fixedDeltaTime)
    {
        Vector3 moveOffset = _directionToTarget.normalized * _moveSpeed * fixedDeltaTime;
        _rigidbody.MovePosition(_transform.position + moveOffset);
    }
    
    public void Rotate(Vector3 target)
    {
        _directionToTarget = target - _transform.position;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;
        _rigidbody.MoveRotation(angle - 90f);
    }
}
