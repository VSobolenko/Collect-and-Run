using System;
using Cysharp.Threading.Tasks;
using Game;
using Game.GUI.Windows;
using Game.Inputs;
using Runner.Common;
using Runner.Levels;
using Runner.UI.PreGameWindow.Components;
using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable CoVariantArrayConversion

namespace Runner.UI.PreGameWindow
{
public class PreGameWindowMediator : BaseReactiveMediator<PreGameWindowView, PreGameWindowAction>
{
    private readonly WindowsDirector _windowsDirector;
    private readonly LevelsDirector _levelsDirector;

    public event Action<PointerEventData, InputEventHandler> OnUserStartGame;

    public PreGameWindowMediator(PreGameWindowView window, WindowsDirector windowsDirector,
                                 LevelsDirector levelsDirector) : base(
        window, window.windowButtons)
    {
        _windowsDirector = windowsDirector;
        _levelsDirector = levelsDirector;
    }

    public override void OnInitialize()
    {
        SubscribeToWindowsButtons();
        window.SetLevelCounterText(_levelsDirector.ActiveLevel.levelId);
        window.inputEventHandler.PointerDown += UserClickMoving;
    }

    public override void OnDestroy()
    {
        UnsubscribeToWindowsButtons();
    }

    private async void UserClickMoving(PointerEventData eventData)
    {
        window.inputEventHandler.PointerDown -= UserClickMoving;
        await UniTask.DelayFrame(1);
        OnUserStartGame?.Invoke(eventData, window.inputEventHandler);
    }

    protected override void ProceedButtonAction(PreGameWindowAction action)
    {
        switch (action)
        {
            case PreGameWindowAction.OnClickShop:
                _windowsDirector.OpenSupportWindow();

                break;
            case PreGameWindowAction.OnClickDiscord:
                ClickOpenDiscord();

                break;
        }
    }

    private static void ClickOpenDiscord()
    {
        Log.Info("Open Discord");
        Application.OpenURL(GameData.DiscordUrl);
    }
}
}