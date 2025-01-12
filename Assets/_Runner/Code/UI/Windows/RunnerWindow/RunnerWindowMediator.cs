using Game.GUI.Windows;
using Game.Inputs;
using Runner.Levels;
using Runner.UI.GameWindow.Components;
using UnityEngine.EventSystems;

// ReSharper disable CoVariantArrayConversion

namespace Runner.UI.GameWindow
{
public class RunnerWindowMediator : BaseReactiveMediator<RunnerWindowView, RunnerWindowAction>
{
    private readonly LevelsDirector _levelsDirector;

    public RunnerWindowMediator(RunnerWindowView window, LevelsDirector levelsDirector) : base(
        window, window.windowButtons)
    {
        _levelsDirector = levelsDirector;
    }

    public override void OnInitialize()
    {
        SubscribeToWindowsButtons();
        window.SetLevelCounterText(_levelsDirector.ActiveLevel.levelId);
    }

    public override void OnDestroy()
    {
        UnsubscribeToWindowsButtons();
    }

    public void InitializePointerEventData(PointerEventData eventData)
    {
        window.inputEventHandler.TryTakeOverControl(window.inputEventHandler, eventData);
    }

    public IAxisDetector GetAxisDetector(PointerEventData starData)
    {
        return new HorizontalDetector(window.inputEventHandler, window.lerpArea, starData);
    }
}
}