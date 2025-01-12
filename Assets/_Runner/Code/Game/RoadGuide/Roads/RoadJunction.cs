using Dreamteck.Splines;
using UnityEngine;

namespace Runner
{
public class RoadJunction : MonoBehaviour
{
    [SerializeField] private Road[] _targets;

    public Road GetNearestRoad(Vector3 from)
    {
        var nearestRoad = _targets[0];
        var minDistance = GetDistanceToFirstPoint(nearestRoad.Spline, from);

        foreach (var target in _targets)
        {
            var distance = GetDistanceToFirstPoint(target.Spline, from);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestRoad = target;
            }
        }

        return nearestRoad;
    }

    private static float GetDistanceToFirstPoint(SplineComputer splineComputer, Vector3 from)
    {
        var firstPoint = splineComputer[0];

        return Vector3.Distance(firstPoint.position, from);
    }
}
}