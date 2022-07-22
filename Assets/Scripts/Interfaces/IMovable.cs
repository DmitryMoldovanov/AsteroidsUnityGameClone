
using UnityEngine;

public interface IMovable
{
    void Move(float fixedDeltaTime);
    void Rotate(Vector3 target);
}
