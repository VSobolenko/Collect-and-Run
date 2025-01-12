using Runner.Collectors.Items.Negative.Types;
using UnityEngine;

namespace Runner.Collectors.Items.Negative
{
public class NegativeSource : MonoBehaviour
{
    [SerializeField] public NegativeType _type;

    public NegativeType Type => _type;

    protected NegativeItem item;

    public void Initialize(NegativeItem item)
    {
        this.item = item;
    }

#if UNITY_EDITOR

    [Space(20), Header("Debug")] [SerializeField]
    private Color _gizmosColor = Color.red;

    [SerializeField] private float _gizmosSize = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        DrawDebug(_gizmosSize);
    }

    protected virtual void DrawDebug(float size)
    {
        var position = transform.position;
        Gizmos.DrawSphere(position, size);
        Gizmos.DrawLine(position, position + transform.forward * size * 1.5f);
    }
#endif
}
}