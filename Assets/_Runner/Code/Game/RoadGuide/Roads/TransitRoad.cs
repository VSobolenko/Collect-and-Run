using Runner.Movements;
using UnityEngine;

namespace Runner
{
[RequireComponent(typeof(RoadJunction))]
public class TransitRoad : Road
{
    [SerializeField, HideInInspector] private RoadJunction _junction;

    protected override void OnRoadFinished(Driver driver)
    {
        var nexRoad = _junction.GetNearestRoad(driver.HorizontalMotor.position);
        nexRoad.Activate(driver);
    }

    private void Reset() => _junction = GetComponent<RoadJunction>();
}
}