using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using WarehouseKeeper._WarehouseKeeper.Scripts.Shops.Monetization.Purchasing.IAP;
using WarehouseKeeper._WarehouseKeeper.Scripts.Shops.Monetization.Purchasing.IAP.UnityServices;

namespace WarehouseKeeper._WarehouseKeeper.Scripts.Shops.Monetization.Purchasing
{
public enum PurchasingClientResult : byte
{
    Success,
    Cancel,
    Error,
}

public readonly struct PurchasingClientResponse
{
    public readonly PurchasingClientResult result;
    public readonly string message;

    public PurchasingClientResponse(PurchasingClientResult result, string message)
    {
        this.result = result;
        this.message = message;
    }
}

public readonly struct PurchasingProductConfig
{
    public readonly string id;
    public readonly ProductType productType;

    public PurchasingProductConfig(string id, ProductType productType)
    {
        this.id = id;
        this.productType = productType;
    }
}

public sealed class PurchasingClientV5
{
    private readonly PurchasingDirectorV5 _director;

    public PurchasingClientV5(IReadOnlyList<PurchasingProductConfig> products)
    {
        var iapCollection = ScriptableObject.CreateInstance<IAPConfigurationCollection>();
        iapCollection.products = BuildConfiguration(products);
        _director = new PurchasingDirectorV5(iapCollection, new UnityServicesManager());
    }

    public async Task<bool> InitializeAsync(CancellationToken token)
    {
        try
        {
            await _director.InitializeAsync(token);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<PurchasingClientResponse> PurchaseAsync(string productId, CancellationToken token)
    {
        try
        {
            var response = await _director.PurchaseProduct(productId, token);

            return new PurchasingClientResponse(ConvertResult(response.result), response.message);
        }
        catch (Exception exception)
        {
            return new PurchasingClientResponse(PurchasingClientResult.Error, exception.Message);
        }
    }

    private static IAPConfigurationData[] BuildConfiguration(IReadOnlyList<PurchasingProductConfig> products)
    {
        if (products == null || products.Count == 0)
            return Array.Empty<IAPConfigurationData>();

        return products.Where(product => string.IsNullOrWhiteSpace(product.id) == false)
                       .Select(product => new IAPConfigurationData
                       {
                           id = product.id,
                           productType = product.productType,
                           purchaseItem = PurchaseItem.None,
                       })
                       .ToArray();
    }

    private static PurchasingClientResult ConvertResult(PurchaseResult result)
    {
        return result switch
        {
            PurchaseResult.Success => PurchasingClientResult.Success,
            PurchaseResult.Cancel => PurchasingClientResult.Cancel,
            _ => PurchasingClientResult.Error,
        };
    }
}
}

