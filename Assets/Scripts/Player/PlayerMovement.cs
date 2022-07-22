using UnityEngine;

public class PlayerMovement : IMovable
{
    private VariableJoystick _joystick;
    
    private float _playerThrustSpeed;
    private float _playerTurnSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private Transform _transform;

    public PlayerMovement(
        Rigidbody2D rigidbody2D,
        VariableJoystick joystick,
        Transform transform,
        float thrustSpeed,
        float turnSpeed)
    {
        _rigidbody = rigidbody2D;
        _joystick = joystick;
        _playerThrustSpeed = thrustSpeed;
        _playerTurnSpeed = turnSpeed;
        _transform = transform;
        
        _moveDirection = new Vector2();
    }

    private Vector2 GetMoveDirection()
    {
        _moveDirection.Set(_joystick.Horizontal, _joystick.Vertical);
        return _moveDirection;
    }

    public void Move(float fixedDeltaTime)
    {
        if (GetMoveDirection() != Vector2.zero)
        {
            _rigidbody.AddForce(GetMoveDirection() * _playerThrustSpeed * fixedDeltaTime, ForceMode2D.Force);
            
            Quaternion target = Quaternion.LookRotation(Vector3.forward, GetMoveDirection());
            Rotate(target.eulerAngles);
        }
    }

    public void Rotate(Vector3 target)
    {
        _rigidbody.SetRotation(
            Quaternion.RotateTowards(
                _transform.rotation,
                Quaternion.Euler(target),
                _playerTurnSpeed * Time.fixedDeltaTime));
    }
}
