using Dreamteck.Splines;
using NaughtyAttributes;
using Runner.PlayerFeature;
using UnityEngine;

namespace Runner.Movements
{
public class Driver : MonoBehaviour
{
    [SerializeField, BoxGroup("Moving")] private SplineFollower _roadFollower;
    [SerializeField, BoxGroup("Moving")] private Transform _horizontalMotor;
    [SerializeField, BoxGroup("Moving")] private Transform _rotationMotor;
    [SerializeField, BoxGroup("Moving")] private Transform _leftLimit;
    [SerializeField, BoxGroup("Moving")] private Transform _rightLimit;

    public SplineFollower Follower => _roadFollower;
    public Transform RotationMotor => _rotationMotor;
    public Transform HorizontalMotor => _horizontalMotor;
    public Transform LeftLimit => _leftLimit;
    public Transform RightLimit => _rightLimit;
    public PlayerMoveComponent MoveComponent { get; set; }

#if UNITY_EDITOR
    [SerializeField, Foldout("Debug")] private Color _gizmosColorRoad = Color.black;
    [SerializeField, Foldout("Debug")] private Color _gizmosColorHorizontal = Color.green;
    [SerializeField, Foldout("Debug")] private Color _gizmosColorLimits = Color.cyan;
    [SerializeField, Foldout("Debug")] private float _gizmosSize = 1;

    private void OnDrawGizmos()
    {
        TryDrawSphere(_roadFollower, _gizmosColorRoad);
        TryDrawSphere(_horizontalMotor, _gizmosColorHorizontal);
        TryDrawSphere(_rightLimit, _gizmosColorLimits);
        TryDrawSphere(_leftLimit, _gizmosColorLimits);
    }

    private void TryDrawSphere(Component component, Color color)
    {
        if (component == null)
            return;
        Gizmos.color = color;
        Gizmos.DrawSphere(component.transform.position, _gizmosSize);
    }
#endif
}
}