using Runner.Collectors.Pickups;
using UnityEngine;

namespace Runner.Collectors.Items.Positive
{
[RequireComponent(typeof(Collider))]
public class PositiveItem : PickupBase
{
    [SerializeField, HideInInspector] private Collider _collider;
    [SerializeField] private PositiveType _type;
    [SerializeField] private int _count;

    private void OnTriggerEnter(Collider enter)
    {
        if (enter.TryGetComponent(out Collector collector) == false)
            return;
        collector.AddPositive(_count);
        SetActive(false);
    }

    private void Reset() => _collider = GetComponent<Collider>();
}
}