using System;
using UnityEngine;

namespace Runner.Movements
{
public class RotateToMoveDirection : IMovable
{
    private readonly Transform _motor;
    private readonly Settings _settings;
    private readonly float _initialRotationY;

    public RotateToMoveDirection(Settings settings, Transform motor)
    {
        _settings = settings;
        _motor = motor;
        _initialRotationY = _motor.localEulerAngles.y;
    }

    public void Move(float direction)
    {
        var targetRotationY = _initialRotationY + direction * _settings.angleOffset;
        var clampedAngle = Mathf.Clamp(targetRotationY, _initialRotationY - _settings.angleOffset,
                                       _initialRotationY + _settings.angleOffset);
        var smoothedAngle = Mathf.LerpAngle(_motor.localEulerAngles.y, clampedAngle,
                                            Time.deltaTime * _settings.rotateSpeed);
        _motor.localEulerAngles = new Vector3(_motor.localEulerAngles.x, smoothedAngle,
                                              _motor.localEulerAngles.z);
    }

    [Serializable]
    public struct Settings
    {
        public float rotateSpeed;
        public float angleOffset;
    }
}
}