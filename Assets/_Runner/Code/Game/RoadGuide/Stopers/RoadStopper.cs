using Runner.Movements;
using UnityEngine;

namespace Runner
{
[RequireComponent(typeof(Collider))]
public class RoadStopper : MonoBehaviour
{
    [SerializeField, HideInInspector] private Collider _collider;

    private void OnTriggerEnter(Collider enter)
    {
        if (enter.TryGetComponent(out Driver driver) == false)
            return;
        driver.MoveComponent.PauseRoadMoving();
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.TryGetComponent(out Driver driver) == false)
            return;
        driver.MoveComponent.ResumeRoadMoving();
    }

    private void Reset() => _collider = GetComponent<Collider>();
}
}