using System;
using R3;
using Runner.Movements;
using UnityEngine;

namespace Runner
{
[RequireComponent(typeof(Collider))]
public class DelayerRoadStopper : MonoBehaviour
{
    [SerializeField, HideInInspector] private Collider _collider;
    [SerializeField] private float _delay = 1;

    private IDisposable _delayedMoving;

    private void OnTriggerEnter(Collider enter)
    {
        if (enter.TryGetComponent(out Driver driver) == false)
            return;
        driver.MoveComponent.PauseMoving();
        _delayedMoving = Observable.Timer(TimeSpan.FromSeconds(_delay))
                                   .Take(1)
                                   .Subscribe(_ => driver.MoveComponent.ResumeMoving())
                                   .AddTo(this);
        DisableInteraction();
    }

    public void Revert()
    {
        _delayedMoving?.Dispose();
        EnableInteraction();
    }

    public void EnableInteraction() => _collider.enabled = true;
    public void DisableInteraction() => _collider.enabled = false;

    private void OnDestroy() => _delayedMoving?.Dispose();

    private void Reset() => _collider = GetComponent<Collider>();
}
}