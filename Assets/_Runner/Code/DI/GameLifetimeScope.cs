using Game.AssetContent;
using Game.Factories;
using Game.GUI.Installers;
using Game.GUI.Windows;
using Game.GUI.Windows.Factories;
using Game.Shops;
using Runner.CameraFeature;
using Runner.Collectors;
using Runner.EntryPoints;
using Runner.Levels;
using Runner.LoopFSM;
using Runner.LoopFSM.States;
using Runner.PlayerFeature;
using Runner.UI;
using Runner.UI.DefeatWindow;
using Runner.UI.GameWindow;
using Runner.UI.PreGameWindow;
using Runner.UI.SupportWindow;
using Runner.UI.VictoryWindow;
using Runner.VContainerImplementation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runner
{
public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private int _levelsCount = 5;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CameraDirector.Settings _settings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameEntryPoint>();
        RegisterSceneComponents(builder);
        RegisterMics(builder);
        RegisterUIWindows(builder);
        RegisterFactories(builder);
        RegisterDirectors(builder);
        RegisterFSMStates(builder);
    }

    private void RegisterSceneComponents(IContainerBuilder builder)
    {
        builder.RegisterComponent(_mainCamera);
        builder.RegisterInstance(_settings);
    }

    private void RegisterUIWindows(IContainerBuilder builder)
    {
        builder.Register<PreGameWindowMediator>(Lifetime.Transient).WithParameter(GetWindow<PreGameWindowView>);
        builder.Register<RunnerWindowMediator>(Lifetime.Transient).WithParameter(GetWindow<RunnerWindowView>);
        builder.Register<VictoryWindowMediator>(Lifetime.Transient).WithParameter(GetWindow<VictoryWindowView>);
        builder.Register<DefeatWindowMediator>(Lifetime.Transient).WithParameter(GetWindow<DefeatWindowView>);
        builder.Register<SupportWindowMediator>(Lifetime.Transient).WithParameter(GetWindow<SupportWindowView>);

        static T GetWindow<T>(IObjectResolver resolver) where T : class
        {
            var installer = resolver.Resolve<MediatorInstantiator>();

            return installer.GetActiveWindow() as T;
        }
    }

    private void RegisterMics(IContainerBuilder builder)
    {
        builder.RegisterInstance<int>(_levelsCount);
        builder.Register<MediatorInstantiator>(Lifetime.Singleton).As<IMediatorInstantiator>().AsSelf();
        builder.Register<IWindowsManagerAsync>(resolver =>
        {
            var gameObjectFactory = resolver.Resolve<IFactoryGameObjects>();
            var resourceManager = resolver.Resolve<IResourceManager>();
            var mediatorInstantiator = resolver.Resolve<IMediatorInstantiator>();

            var windowFactory = GuiInstaller.WindowFactory(mediatorInstantiator, resourceManager, gameObjectFactory);

            return GuiInstaller.ManagerAsync(windowFactory, GuiInstaller.Empty(), null);
        }, Lifetime.Transient);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<VContainerFactoryGameObjects>(Lifetime.Singleton).As<IFactoryGameObjects>();
        builder.Register<LevelsFactory>(Lifetime.Scoped);
        builder.Register<PickupFactory>(Lifetime.Scoped);
        builder.Register<PlayerFactory>(Lifetime.Scoped);
    }

    private void RegisterDirectors(IContainerBuilder builder)
    {
        builder.Register<LevelsDirector>(Lifetime.Scoped);
        builder.Register<PickupsDirector>(Lifetime.Scoped);
        builder.Register<WindowsDirector>(Lifetime.Scoped);
        builder.Register<ShopDirector>(Lifetime.Scoped).As<IInitializable, ShopDirector>();
        builder.Register<PlayerDirector>(Lifetime.Scoped).As<ITickable, PlayerDirector>();
        builder.Register<CameraDirector>(Lifetime.Scoped).As<ILateTickable, CameraDirector>();
    }

    private void RegisterFSMStates(IContainerBuilder builder)
    {
        builder.Register<GameFSM>(Lifetime.Singleton);
        builder.Register<EntryPointState>(Lifetime.Transient).AsSelf().WithParameter(resolver =>
        {
            var gamFSM = resolver.Resolve<GameFSM>();

            return gamFSM.ActualFSM;
        });
        builder.Register<RunnerState>(Lifetime.Transient).AsSelf()
               .WithParameter(resolver => resolver.Resolve<GameFSM>().ActualFSM);
        builder.Register<DefeatState>(Lifetime.Transient)
               .WithParameter(resolver => resolver.Resolve<GameFSM>().ActualFSM);
        builder.Register<VictoryState>(Lifetime.Transient).AsSelf()
               .WithParameter(resolver => resolver.Resolve<GameFSM>().ActualFSM);
        builder.Register<DisposeState>(Lifetime.Transient).AsSelf()
               .WithParameter(resolver => resolver.Resolve<GameFSM>().ActualFSM);
    }
}
}