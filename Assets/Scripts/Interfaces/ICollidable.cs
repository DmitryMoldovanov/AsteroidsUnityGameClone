using UnityEngine;

public interface ICollidable
{
    bool IsPlayerInsideViewRange();
    bool IsPlayerInsideAttackRange();
    Vector3 GetTargetPosition();
}
