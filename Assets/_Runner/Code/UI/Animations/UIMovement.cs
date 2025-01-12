using DG.Tweening;
using UnityEngine;

namespace Runner.UI.Animations
{
public class UIMovement : MonoBehaviour
{
    [SerializeField] private Transform _toTarget;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private int _repeatCount = -1;
    [SerializeField] private Ease _ease = Ease.InBack;
    [SerializeField] private LoopType _loop = LoopType.Restart;

    private Tween _tween;

    private void Start()
    {
        _tween = transform.DOMove(_toTarget.position, _moveSpeed).SetLoops(_repeatCount, _loop).SetEase(_ease);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
}