using Game.Extensions;
using Game.Inputs;
using UnityEngine;

namespace Runner.Movements
{
public class HorizontalMovement : IMovable
{
    private readonly IAxisDetector _axisDetector;
    private readonly IMovable _viewToDirection;
    private readonly Transform _motor;
    private readonly Vector3 _leftLimit;
    private readonly Vector3 _rightLimit;

    public HorizontalMovement(Transform motor, Vector3 leftLimit, Vector3 rightLimit, IAxisDetector axisDetector,
                              IMovable viewToDirection)
    {
        _leftLimit = leftLimit;
        _rightLimit = rightLimit;
        _axisDetector = axisDetector;
        _viewToDirection = viewToDirection;
        _motor = motor;
    }

    public void Move(float speed)
    {
        _motor.Translate(Time.deltaTime * speed * _axisDetector.AxisNormalized * Vector3.right, Space.Self);
        var clamp = Mathf.Clamp(_motor.localPosition.x, _leftLimit.x, _rightLimit.x);
        _motor.SetLocalX(clamp);
        _viewToDirection.Move(_axisDetector.AxisNormalized);
    }
}
}