using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseKeeper._WarehouseKeeper.Scripts.Shops.Monetization.Purchasing;

#pragma warning disable 8632

namespace Game.Shops
{
internal class IAPShopManager : IShopManager
{
    private readonly GameProduct[] _sourceProducts;
    private readonly PurchasingClientV5 _purchasingClient;

    private Task<bool>? _initializationTask;

    public HashSet<GameProduct> Products { get; private set; }

    public IAPShopManager(GameProduct[] sourceProducts)
    {
        _sourceProducts = sourceProducts;
        _purchasingClient = new PurchasingClientV5(BuildProductConfigs(sourceProducts));
    }

    public Task<bool> Initialize()
    {
        if (_initializationTask != null)
            return _initializationTask;

        _initializationTask = InitializeInternal();

        return _initializationTask;
    }

    public async Task<PurchaseResponseResult> PurchaseProduct(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return new PurchaseResponseResult
            {
                result = PurchaseResult.Error,
                message = "ProductId is null or empty",
            };
        }

        if (_initializationTask == null)
            await Initialize();

        var purchaseResult = await _purchasingClient.PurchaseAsync(productId, CancellationToken.None);

        return new PurchaseResponseResult
        {
            result = ConvertResult(purchaseResult.result),
            message = purchaseResult.message,
        };
    }

    private async Task<bool> InitializeInternal()
    {
        Products = BuildProducts(_sourceProducts);

        if (_sourceProducts == null)
        {
            Log.Error("Null source products");

            return false;
        }

        return await _purchasingClient.InitializeAsync(CancellationToken.None);
    }

    private static HashSet<GameProduct> BuildProducts(GameProduct[] products)
    {
        if (products == null)
            return new HashSet<GameProduct>();

        return products.Where(product => product != null && product.Ignored == false)
                       .ToHashSet();
    }

    private static PurchasingProductConfig[] BuildProductConfigs(GameProduct[] products)
    {
        if (products == null)
            return Array.Empty<PurchasingProductConfig>();

        return products.Where(product => product != null && product.Ignored == false)
                       .Select(product => new PurchasingProductConfig(product.ProductId, ConvertProductType(product.Type)))
                       .ToArray();
    }

    private static UnityEngine.Purchasing.ProductType ConvertProductType(ProductType type)
    {
        return type switch
        {
            ProductType.Consumable => UnityEngine.Purchasing.ProductType.Consumable,
            ProductType.NonConsumable => UnityEngine.Purchasing.ProductType.NonConsumable,
            ProductType.Subscription => UnityEngine.Purchasing.ProductType.Subscription,
            _ => UnityEngine.Purchasing.ProductType.Consumable,
        };
    }

    private static PurchaseResult ConvertResult(PurchasingClientResult result)
    {
        return result switch
        {
            PurchasingClientResult.Success => PurchaseResult.Success,
            PurchasingClientResult.Cancel => PurchaseResult.Cancel,
            _ => PurchaseResult.Error,
        };
    }
}
}