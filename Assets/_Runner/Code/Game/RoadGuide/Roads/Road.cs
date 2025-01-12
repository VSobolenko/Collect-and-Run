using System;
using Dreamteck.Splines;
using Runner.Movements;
using UnityEngine;

namespace Runner
{
public class Road : MonoBehaviour
{
    [SerializeField] private SplineComputer _road;

    private Driver _driver;

    public SplineComputer Spline => _road;
    public event Action OnRoadStart;
    public event Action OnRoadFinish;

    public SplineComputer Activate(Driver roadDriver)
    {
        if (_driver != null)
            throw new InvalidOperationException("Road already activated");
        _driver = roadDriver;

        var startPosition = _driver.HorizontalMotor.position;
        _driver.Follower.spline = _road;
        _driver.Follower.Restart();
        _driver.Follower.RebuildImmediate();
        _driver.Follower.SetPercent(0);
        _driver.HorizontalMotor.position = startPosition;
        _driver.Follower.onEndReached += OnFinishRoad;
        OnRoadActivated(_driver);
        OnRoadStart?.Invoke();

        return _road;
    }

    private void OnFinishRoad(double d)
    {
        _driver.Follower.onEndReached -= OnFinishRoad;
        OnRoadFinished(_driver);
        OnRoadFinish?.Invoke();
        _driver = null;
    }

    protected virtual void OnRoadActivated(Driver driver) { }
    protected virtual void OnRoadFinished(Driver driver) { }
}
}