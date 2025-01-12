using VContainer.Unity;

namespace Game.Shops
{
public class ShopDirector : IInitializable
{
    private readonly IShopManager _shopManager;

    public IShopManager ShopManager => _shopManager;

    public ShopDirector(IShopManager shopManager)
    {
        _shopManager = shopManager;
    }

    public void Initialize()
    {
        _shopManager.Initialize();
    }
}
}