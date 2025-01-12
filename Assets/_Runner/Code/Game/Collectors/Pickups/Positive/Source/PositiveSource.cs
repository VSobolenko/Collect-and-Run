using UnityEngine;

namespace Runner.Collectors.Items.Positive
{
public class PositiveSource : MonoBehaviour
{
    [SerializeField] public PositiveType _type;

    public PositiveType Type => _type;

    protected PositiveItem item;

    public void Initialize(PositiveItem item)
    {
        this.item = item;
    }

#if UNITY_EDITOR
    [Space(20), Header("Debug")] [SerializeField]
    private Color _gizmosColor = Color.green;

    [SerializeField] private float _gizmosSize = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        DrawGizmos(_gizmosSize);
    }

    protected virtual void DrawGizmos(float size)
    {
        var position = transform.position;
        Gizmos.DrawSphere(position, size);
        Gizmos.DrawLine(position, position + transform.forward * size * 1.5f);
    }
#endif
}
}