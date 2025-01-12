using System;
using Game.GUI.Windows;
using Runner.Levels;
using Runner.LoopFSM;
using Runner.UI.VictoryWindow.Components;

// ReSharper disable CoVariantArrayConversion

namespace Runner.UI.VictoryWindow
{
public class VictoryWindowMediator : BaseReactiveMediator<VictoryWindowView, VictoryWindowAction>
{
    private readonly GameFSM _fsm;
    private readonly WindowsDirector _windowsDirector;
    private readonly LevelsDirector _levelsDirector;

    public VictoryWindowMediator(VictoryWindowView window, GameFSM fsm, WindowsDirector windowsDirector,
                                 LevelsDirector levelsDirector) : base(
        window, window.windowButtons)
    {
        _fsm = fsm;
        _windowsDirector = windowsDirector;
        _levelsDirector = levelsDirector;
    }

    public override void OnInitialize()
    {
        window.SetLevelCounterText(_levelsDirector.ActiveLevel.levelId);
        SubscribeToWindowsButtons();
    }

    public override void OnDestroy()
    {
        UnsubscribeToWindowsButtons();
    }

    protected override void ProceedButtonAction(VictoryWindowAction action)
    {
        switch (action)
        {
            case VictoryWindowAction.Unknown:
                break;
            case VictoryWindowAction.OnClickGet:
                ProcessRestartGame();

                break;
            case VictoryWindowAction.OnClickGetSemicircleFortune:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    private void ProcessRestartGame()
    {
        //_windowsDirector.CloseWindow(this);
        _fsm.DisposeActiveFsm();
        _fsm.StartFSM();
    }
}
}