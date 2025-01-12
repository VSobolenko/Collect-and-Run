using System;
using Game.Inputs;
using Runner.Movements;
using UnityEngine;

namespace Runner
{
public class Highway : MonoBehaviour
{
    [SerializeField] private Road _startRoad;
    [SerializeField] private Road _endRoad;
    [SerializeField] private Highway _next;

    public event Action OnHighwayEnd;
    private Driver _driver;
    private IAxisDetector _axisDetector;

    public Road StartPoint => _startRoad;

    public void Run(Driver driver, IAxisDetector axisDetector)
    {
        _driver = driver;
        _axisDetector = axisDetector;

        _startRoad.Activate(driver);
        _endRoad.OnRoadFinish += TryToDriveNextHighway;
    }

    private void TryToDriveNextHighway()
    {
        _endRoad.OnRoadFinish -= TryToDriveNextHighway;
        if (_next != null)
            _next.Run(_driver, _axisDetector);
        else
            OnHighwayEnd?.Invoke();
    }
}
}