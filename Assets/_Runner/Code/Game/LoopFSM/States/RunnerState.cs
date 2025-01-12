using Game.FSMCore.Machines;
using Game.FSMCore.States;
using Runner.PlayerFeature;
using Runner.UI;
using Runner.UI.GameWindow;

namespace Runner.LoopFSM.States
{
public class RunnerState : DeadState<Enter2RunnerArgs>
{
    private readonly PlayerDirector _playerDirector;
    private readonly WindowsDirector _windowsDirector;
    private readonly LiteStateMachine _stateMachine;

    public RunnerState(PlayerDirector playerDirector, WindowsDirector windowsDirector, LiteStateMachine stateMachine)
    {
        _playerDirector = playerDirector;
        _windowsDirector = windowsDirector;
        _stateMachine = stateMachine;
    }

    protected override void OnStateActivated()
    {
        var gameWindow = _windowsDirector.OpenGameWindow();
        gameWindow.InitializePointerEventData(inputData.eventData);

        var axisDetector = gameWindow.GetAxisDetector(inputData.eventData);
        _playerDirector.Player.view.Progress.SetActive(true);
        _playerDirector.StartMoving(inputData.level, axisDetector);
        _playerDirector.OnLooser += ProcessLooser;
        inputData.level.OnLevelHighwayEnd += ProcessToVictoryState;
    }

    private void ProcessLooser() => _stateMachine.TransitTo<DefeatState>();

    private void ProcessToVictoryState() => _stateMachine.TransitTo<VictoryState>();

    public override void Dispose()
    {
        inputData.level.OnLevelHighwayEnd -= ProcessToVictoryState;
        _playerDirector.OnLooser -= ProcessLooser;
        _windowsDirector.CloseWindow<RunnerWindowMediator>();
    }
}
}