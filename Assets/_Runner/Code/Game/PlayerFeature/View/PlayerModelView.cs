using Game.Pools;
using UnityEngine;

namespace Runner.PlayerFeature.View
{
public class PlayerModelView : MonoPooledObject
{
    [SerializeField] private PlayerAnimator _animator;
    public PlayerAnimator Animation => _animator;
}
}