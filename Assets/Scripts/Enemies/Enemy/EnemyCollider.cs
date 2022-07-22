using UnityEngine;

public class EnemyCollider : ICollidable
{
    private Transform _transform;
    private float _viewRange;
    private float _attackRange;
    private LayerMask _playerMask;
    
    private Vector3 _targetPosition;

    public EnemyCollider(Transform transform, float viewRange, float attackRange, LayerMask layerMask)
    {
        _transform = transform;
        _viewRange = viewRange;
        _attackRange = attackRange;
        _playerMask = layerMask;
    }
    
    public bool IsPlayerInsideViewRange()
    {
        bool collision = Physics2D.
            CircleCast(_transform.position, _viewRange, Vector2.zero, _viewRange, _playerMask);
        return collision;
    }

    public bool IsPlayerInsideAttackRange()
    {
        bool collision = Physics2D.
            CircleCast(_transform.position, _attackRange, Vector2.zero, _attackRange, _playerMask);
        return collision;
    }
    
    public Vector3 GetTargetPosition()
    {
        var target = Physics2D.
            CircleCast(_transform.position, _viewRange, Vector2.zero, _attackRange, _playerMask);

        return target.transform != null ? _targetPosition = target.transform.position : _targetPosition;
    }
}
