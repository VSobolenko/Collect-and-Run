using UnityEngine;

namespace Runner.PlayerFeature.View
{
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int _speed = Animator.StringToHash("Speed");
    private readonly int _victoryDance = Animator.StringToHash("VictoryDance");
    private readonly int _defeatDance = Animator.StringToHash("DefeatDance");
    private readonly int _happy = Animator.StringToHash("Happy");
    private readonly int _degreeOfHappiness = Animator.StringToHash("DegreeOfHappiness");
    private readonly int _sadness = Animator.StringToHash("Sadness");
    private readonly int _degreeOfSadness = Animator.StringToHash("DegreeOfSadness");

    public void Idle()
    {
        _animator.Play($"Idle");
    }

    public void Walk(float speed) => _animator.SetFloat(_speed, speed);

    public void VictoryDance() => _animator.SetTrigger(_victoryDance);

    public void DefeatDance() => _animator.SetTrigger(_defeatDance);

    public void Happy(int currentHappy, int maxHappy)
    {
        _animator.SetFloat(_degreeOfHappiness, maxHappy / (float) currentHappy);
        _animator.SetTrigger(_happy);
    }

    public void Sadness(int currentHappy, int maxHappy)
    {
        _animator.SetFloat(_degreeOfSadness, maxHappy / (float) currentHappy);
        _animator.SetTrigger(_sadness);
    }

    public void ResetAnimationToDefault()
    {
        _animator.Play($"Idle");
    }

    private void Reset()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void ResetPositions() => _animator.transform.localPosition = Vector3.zero;

    public void SetActiveRootMotion(bool value) => _animator.applyRootMotion = value;
}
}