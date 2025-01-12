using Game.Inputs;
using Runner.Levels;
using Runner.Movements;
using Runner.PlayerFeature.Configs;

namespace Runner.PlayerFeature
{
public class PlayerMoveComponent
{
    private readonly Driver _driver;
    private MovementSystem _movementSystem;
    private readonly PlayerConfig _playerConfig;

    public PlayerMoveComponent(Driver driver, PlayerConfig playerConfig)
    {
        _driver = driver;
        _playerConfig = playerConfig;
    }

    public void StartMoving(Level level, IAxisDetector axisDetector)
    {
        InitializeMoveSystem(axisDetector);
        level.startHighway.Run(_driver, axisDetector);
    }

    private void InitializeMoveSystem(IAxisDetector axisDetector)
    {
        var roadMover = new SplineRoad(_driver.Follower);
        var rotation = _movementSystem == null
            ? new RotateToMoveDirection(_playerConfig.rotationSettings, _driver.RotationMotor)
            : _movementSystem[typeof(RotateToMoveDirection)].behaviour;

        var horizontalMover = new HorizontalMovement(_driver.HorizontalMotor, _driver.LeftLimit.localPosition,
                                                     _driver.RightLimit.localPosition, axisDetector, rotation);
        _movementSystem = new MovementSystem()
                          .AddMovable(new MoveSetup
                          {
                              behaviour = roadMover,
                              isPause = false,
                              speed = _playerConfig.roadSpeed,
                          })
                          .AddMovable(new MoveSetup
                          {
                              behaviour = horizontalMover,
                              isPause = false,
                              speed = _playerConfig.horizontalSpeed,
                          }).AddMovable(new MoveSetup
                          {
                              behaviour = rotation,
                              isPause = true,
                              speed = 0,
                          });
    }

    public void PauseRoadMoving() => SetPause<SplineRoad>(true);
    public void ResumeRoadMoving() => SetPause<SplineRoad>(false);
    public void PauseMoving() => SetPause(true);
    public void ResumeMoving() => SetPause(false);

    public void StopMoving()
    {
        _driver.Follower.spline = null;
        _driver.Follower.RebuildImmediate();
        _movementSystem.Clear();
    }

    public void Update() => _movementSystem?.Update();

    private void SetPause<T>(bool value) => _movementSystem[typeof(T)].isPause = value;
    private void SetPause(bool value) => _movementSystem.IsPaused = value;
}
}