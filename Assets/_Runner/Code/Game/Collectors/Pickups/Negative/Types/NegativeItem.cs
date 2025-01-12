using Runner.Collectors.Pickups;
using UnityEngine;

namespace Runner.Collectors.Items.Negative.Types
{
[RequireComponent(typeof(Collider))]
public class NegativeItem : PickupBase
{
    [SerializeField, HideInInspector] private Collider _collider;
    [SerializeField] private NegativeType _type;
    [SerializeField] private int _count;

    private void OnTriggerEnter(Collider enter)
    {
        if (enter.TryGetComponent(out Collector collector) == false)
            return;
        collector.AddNegative(_count);
        SetActive(false);
    }

    private void Reset() => _collider = GetComponent<Collider>();
}
}