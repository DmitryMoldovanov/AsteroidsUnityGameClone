using UnityEngine;

public interface IPatrolable
{
    Vector2 CalculatePatrolPoint();
    void Patrol(Vector3 pointToPatrolAround, float fixedDeltaTime);
}
