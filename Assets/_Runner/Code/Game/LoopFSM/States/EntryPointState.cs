using Game.FSMCore.Machines;
using Game.FSMCore.States;
using Game.Inputs;
using Runner.Analytics;
using Runner.CameraFeature;
using Runner.Levels;
using Runner.PlayerFeature;
using Runner.UI;
using Runner.UI.PreGameWindow;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runner.LoopFSM.States
{
public struct Enter2RunnerArgs
{
    public Level level;
    public PointerEventData eventData;
}

public class EntryPointState : QuiteState
{
    private readonly LevelsDirector _levelsDirector;
    private readonly PlayerDirector _playerDirector;
    private readonly WindowsDirector _windowsDirector;
    private readonly LiteStateMachine _stateMachine;
    private readonly CameraDirector _cameraDirector;
    private readonly IAnalyticsDirector _analyticsDirector;

    private Level _loadedLevel;
    private PreGameWindowMediator _uiMediator;

    public EntryPointState(LevelsDirector levelsDirector, PlayerDirector playerDirector,
                           WindowsDirector windowsDirector, LiteStateMachine stateMachine,
                           CameraDirector cameraDirector, IAnalyticsDirector analyticsDirector)
    {
        _levelsDirector = levelsDirector;
        _playerDirector = playerDirector;
        _windowsDirector = windowsDirector;
        _stateMachine = stateMachine;
        _cameraDirector = cameraDirector;
        _analyticsDirector = analyticsDirector;
    }

    protected override void OnStateActivated()
    {
        _playerDirector.CreateDefaultPlayerInPosition(Vector3.zero);
        _cameraDirector.FollowToTarget(_playerDirector.Player.view.Driver.HorizontalMotor);
        _playerDirector.Player.view.Progress.SetActive(false);
        var levelData = _levelsDirector.LoadLevel();
        _analyticsDirector.LevelStart(levelData.levelId);
        _loadedLevel = levelData.level;
        _uiMediator = _windowsDirector.OpenPreGameWindow();
        _uiMediator.OnUserStartGame += ProcessStartGameMoving;
    }

    private void ProcessStartGameMoving(PointerEventData eventData, InputEventHandler inputEventHandler)
    {
        _stateMachine.TransitTo<RunnerState, Enter2RunnerArgs>(new Enter2RunnerArgs
        {
            level = _loadedLevel,
            eventData = eventData,
        });
    }

    public override void Dispose()
    {
        _uiMediator.OnUserStartGame -= ProcessStartGameMoving;
        _windowsDirector.CloseWindow<PreGameWindowMediator>();
    }
}
}