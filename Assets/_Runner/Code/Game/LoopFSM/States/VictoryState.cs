using Game.FSMCore.States;
using Runner.Analytics;
using Runner.CameraFeature;
using Runner.Levels;
using Runner.PlayerFeature;
using Runner.UI;
using Runner.UI.VictoryWindow;

namespace Runner.LoopFSM.States
{
public class VictoryState : QuiteState
{
    private readonly WindowsDirector _windowsDirector;
    private readonly PlayerDirector _playerDirector;
    private readonly LevelsDirector _levelsDirector;
    private readonly CameraDirector _cameraDirector;
    private readonly IAnalyticsDirector _analyticsDirector;

    public VictoryState(WindowsDirector windowsDirector, PlayerDirector playerDirector, LevelsDirector levelsDirector,
                        CameraDirector cameraDirector, IAnalyticsDirector analyticsDirector)
    {
        _windowsDirector = windowsDirector;
        _playerDirector = playerDirector;
        _levelsDirector = levelsDirector;
        _cameraDirector = cameraDirector;
        _analyticsDirector = analyticsDirector;
    }

    protected override void OnStateActivated()
    {
        _playerDirector.Player.view.Progress.SetActive(false);
        _playerDirector.StopMoving();
        _playerDirector.Victory();
        _playerDirector.Player.model.Animation.SetActiveRootMotion(true);
        _analyticsDirector.LevelVictory(_levelsDirector.ActiveLevel.levelId);
        _windowsDirector.OpenVictoryWindow();
        _cameraDirector.RotateAroundTarget(_playerDirector.Player.view.Driver.HorizontalMotor);
    }

    public override void Dispose()
    {
        _playerDirector.Player.model.Animation.ResetPositions();
        _playerDirector.Player.model.Animation.SetActiveRootMotion(false);
        _levelsDirector.MarkActiveLevelComplete();
        _windowsDirector.CloseWindow<VictoryWindowMediator>();
    }
}
}