using System;
using UnityEngine;
using VContainer.Unity;

namespace Runner.CameraFeature
{
public class CameraDirector : ILateTickable
{
    private readonly Camera _camera;
    private readonly Settings _settings;

    private CameraMode _cameraMode;
    private Transform _target;

    public CameraDirector(Camera camera, Settings settings)
    {
        _camera = camera;
        _settings = settings;
    }

    public void FollowToTarget(Transform target)
    {
        _target = target;
        _cameraMode = CameraMode.FollowTo;
    }

    public void RotateAroundTarget(Transform target)
    {
        _target = target;
        _cameraMode = CameraMode.RotateAround;
    }

    public void StopCameraMotion()
    {
        _cameraMode = CameraMode.None;
    }

    public void LateTick()
    {
        switch (_cameraMode)
        {
            case CameraMode.None:
                break;
            case CameraMode.RotateAround:
                RotateAroundObject();

                break;
            case CameraMode.FollowTo:
                FollowToTarget();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void RotateAroundObject()
    {
        _camera.transform.RotateAround(_target.position, Vector3.up, _settings.rotationSpeed * Time.deltaTime);
        _camera.transform.LookAt(_target.position + _settings.lookOffset);
    }

    private void FollowToTarget()
    {
        var desiredPosition = _target.position + _target.rotation * _settings.positionOffset;
        var smoothedPosition = Vector3.Lerp(_camera.transform.position, desiredPosition, _settings.smoothSpeed);

        _camera.transform.position = smoothedPosition;
        _camera.transform.LookAt(_target.position + _settings.lookOffset);
    }

    [Serializable]
    public class Settings
    {
        public Vector3 positionOffset = new(0, 2.2f, -3.4f);
        public float smoothSpeed = 0.125f;
        public float rotationSpeed = 40f;
        public Vector3 lookOffset = new(0, 0.8f, 0);
    }

    private enum CameraMode : byte
    {
        None,
        RotateAround,
        FollowTo,
    }
}
}