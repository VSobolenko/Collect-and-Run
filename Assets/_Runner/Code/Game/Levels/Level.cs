using System;
using NaughtyAttributes;
using Runner.Collectors.Items.Negative;
using Runner.Collectors.Items.Positive;
using UnityEngine;

namespace Runner.Levels
{
public class Level : MonoBehaviour
{
    public Vector3 startPoint;
    public Highway startHighway;
    public Highway endHighway;
    public PositiveSource[] positiveItems;
    public NegativeSource[] negativeSources;

    public event Action OnLevelHighwayEnd;

    private void Start() => endHighway.OnHighwayEnd += LevelHighwayEnd;

    private void OnDestroy() => endHighway.OnHighwayEnd -= LevelHighwayEnd;

    private void LevelHighwayEnd() => OnLevelHighwayEnd?.Invoke();

#if UNITY_EDITOR

    [ContextMenu("Collect items"), Button,]
    private void CollectItems()
    {
        positiveItems = GetComponentsInChildren<PositiveSource>(true);
        negativeSources = GetComponentsInChildren<NegativeSource>(true);
        UnityEditor.EditorUtility.SetDirty(this);
    }

    [SerializeField, Foldout("Debug")] private Transform _configureStartPointByTransform;
    [SerializeField, Foldout("Debug")] private Color _gizmosColor = Color.blue;
    [SerializeField, Foldout("Debug")] private float _gizmosSize = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        if (_configureStartPointByTransform != null)
            startPoint = _configureStartPointByTransform.position;
        Gizmos.DrawSphere(startPoint, _gizmosSize);
    }
#endif
}
}