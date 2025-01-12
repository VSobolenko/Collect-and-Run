using System;
using Game.GUI.Windows;
using Runner.LoopFSM;
using Runner.UI.DefeatWindow.Components;

// ReSharper disable CoVariantArrayConversion

namespace Runner.UI.DefeatWindow
{
public class DefeatWindowMediator : BaseReactiveMediator<DefeatWindowView, DefeatWindowAction>
{
    private readonly GameFSM _fsm;
    private readonly WindowsDirector _windowsDirector;

    public DefeatWindowMediator(DefeatWindowView window, GameFSM fsm, WindowsDirector windowsDirector) : base(
        window, window.windowButtons)
    {
        _fsm = fsm;
        _windowsDirector = windowsDirector;
    }

    public override void OnInitialize()
    {
        SubscribeToWindowsButtons();
    }

    public override void OnDestroy()
    {
        UnsubscribeToWindowsButtons();
    }

    protected override void ProceedButtonAction(DefeatWindowAction action)
    {
        switch (action)
        {
            case DefeatWindowAction.Unknown:
                break;
            case DefeatWindowAction.OnClickShop:
                break;
            case DefeatWindowAction.OnClickRetry:
                ProcessRestartGame();

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