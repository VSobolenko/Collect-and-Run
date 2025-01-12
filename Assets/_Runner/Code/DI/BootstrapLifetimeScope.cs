using Game.AssetContent;
using Game.AssetContent.Installers;
using Game.Factories;
using Game.Pools;
using Game.Pools.Installers;
using Game.Shops;
using Game.Shops.Installers;
using Runner.Analytics;
using Runner.VContainerImplementation;
using VContainer;
using VContainer.Unity;

namespace Runner
{
public class BootstrapLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        BindManagers(builder);
        BindDirectors(builder);
    }

    private void BindManagers(IContainerBuilder builder)
    {
        builder.Register<VContainerFactoryGameObjects>(Lifetime.Singleton).As<IFactoryGameObjects>();
        builder.RegisterInstance<IResourceManager>(ResourceManagerInstaller.Addressable());
        builder.RegisterInstance<IShopManager>(ShopInstaller.IAP());
        builder.Register<IObjectPoolManager>(resolver =>
        {
            var factoryGameObjects = resolver.Resolve<IFactoryGameObjects>();

            return ObjectPoolInstaller.KeyAutoEditor(factoryGameObjects);
        }, Lifetime.Scoped);
    }

    private void BindDirectors(IContainerBuilder builder)
    {
        builder.Register<GameAnalyticsDirector>(Lifetime.Singleton).As<IAnalyticsDirector>();
    }
}
}