using UnityEngine;

namespace Runner.Collectors.Items.Negative
{
public class NegativePatrolSource : NegativeSource
{
    [SerializeField] private Transform[] _patrolPoints;

#if UNITY_EDITOR
    protected override void DrawDebug(float size)
    {
        base.DrawDebug(size);
        for (var i = 0; i < _patrolPoints.Length; i++)
        {
            var pointPosition = _patrolPoints[i].position;
            Gizmos.DrawSphere(pointPosition, size / 2f);
            if (i > 0)
                Gizmos.DrawLine(pointPosition, _patrolPoints[i - 1].position);
        }
    }
#endif
}
}