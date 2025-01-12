using Game.GUI.Windows;
using Game.GUI.Windows.Factories;
using VContainer;

namespace Runner.UI
{
public class MediatorInstantiator : IMediatorInstantiator
{
    private readonly IObjectResolver _resolver;

    private WindowUI _windowForVContainer;

    public MediatorInstantiator(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    public TMediator Instantiate<TMediator>(WindowUI windowUI) where TMediator : class, IMediator
    {
        _windowForVContainer = windowUI;

        return _resolver.Resolve<TMediator>();
    }

    public WindowUI GetActiveWindow() => _windowForVContainer;
}
}