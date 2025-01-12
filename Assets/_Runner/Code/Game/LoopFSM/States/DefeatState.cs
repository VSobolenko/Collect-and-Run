using Game.FSMCore.States;
using Runner.Analytics;
using Runner.Levels;
using Runner.PlayerFeature;
using Runner.UI;
using Runner.UI.DefeatWindow;

namespace Runner.LoopFSM.States
{
public class DefeatState : QuiteState
{
    private readonly WindowsDirector _windowsDirector;
    private readonly PlayerDirector _playerDirector;
    private readonly IAnalyticsDirector _analyticsDirector;
    private readonly LevelsDirector _levelsDirector;

    public DefeatState(WindowsDirector windowsDirector, PlayerDirector playerDirector,
                       IAnalyticsDirector analyticsDirector, LevelsDirector levelsDirector)
    {
        _windowsDirector = windowsDirector;
        _playerDirector = playerDirector;
        _analyticsDirector = analyticsDirector;
        _levelsDirector = levelsDirector;
    }

    protected override void OnStateActivated()
    {
        _analyticsDirector.LevelDefeat(_levelsDirector.ActiveLevel.levelId);
        _playerDirector.Player.view.Progress.SetActive(false);
        _playerDirector.StopMoving();
        _playerDirector.Defeat();
        _playerDirector.Player.model.Animation.SetActiveRootMotion(true);
        _windowsDirector.OpenDefeatWindow();
    }

    public override void Dispose()
    {
        _playerDirector.Player.model.Animation.ResetPositions();
        _playerDirector.Player.model.Animation.SetActiveRootMotion(false);
        _windowsDirector.CloseWindow<DefeatWindowMediator>();
    }
}
}