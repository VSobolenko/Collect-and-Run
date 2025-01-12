using Game.GUI.Windows;
using Runner.UI.DefeatWindow;
using Runner.UI.GameWindow;
using Runner.UI.PreGameWindow;
using Runner.UI.SupportWindow;
using Runner.UI.VictoryWindow;

namespace Runner.UI
{
public class WindowsDirector
{
    private readonly IWindowsManagerAsync _windowsManagerAsync;

    public WindowsDirector(IWindowsManagerAsync windowsManagerAsync)
    {
        _windowsManagerAsync = windowsManagerAsync;
    }

    public PreGameWindowMediator OpenPreGameWindow() => _windowsManagerAsync.OpenWindowOnTop<PreGameWindowMediator>();
    public RunnerWindowMediator OpenGameWindow() => _windowsManagerAsync.OpenWindowOnTop<RunnerWindowMediator>();
    public VictoryWindowMediator OpenVictoryWindow() => _windowsManagerAsync.OpenWindowOnTop<VictoryWindowMediator>();
    public DefeatWindowMediator OpenDefeatWindow() => _windowsManagerAsync.OpenWindowOnTop<DefeatWindowMediator>();
    public SupportWindowMediator OpenSupportWindow() => _windowsManagerAsync.OpenWindowOnTop<SupportWindowMediator>();

    public T GetWindow<T>() where T : class, IMediator
    {
        _windowsManagerAsync.TryGetActiveWindow<T>(out var mediator);

        return mediator;
    }

    public void CloseWindow(IMediator mediator) => _windowsManagerAsync.CloseWindow(mediator);
    public void CloseWindow<T>() where T : class, IMediator => _windowsManagerAsync.CloseWindow<T>();
}
}