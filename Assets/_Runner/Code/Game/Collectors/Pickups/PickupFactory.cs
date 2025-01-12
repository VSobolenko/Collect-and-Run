using System;
using System.Linq;
using Game.AssetContent;
using Game.Pools;
using Runner.Collectors.Items.Negative;
using Runner.Collectors.Items.Negative.Types;
using Runner.Collectors.Items.Positive;
using Runner.Collectors.Pickups;
using UnityEngine;

namespace Runner.Collectors
{
public class PickupFactory
{
    private readonly IObjectPoolManager _objectPool;
    private readonly IResourceManager _resourceManager;

    public PickupFactory(IObjectPoolManager objectPool, IResourceManager resourceManager)
    {
        _objectPool = objectPool;
        _resourceManager = resourceManager;
        PreparePickupItemsWithExclude<PositiveType>("Positive", PositiveType.Unknown, PositiveType.Money,
                                                    PositiveType.Casino, PositiveType.Dollar);
        PreparePickupItemsWithExclude<NegativeType>("Negative", NegativeType.Unknown, NegativeType.Bill,
                                                    NegativeType.Smoking, NegativeType.Jailer, NegativeType.Wine,
                                                    NegativeType.Garbage);
    }

    public PositiveItem GetPositiveItem(PositiveSource source)
    {
        var gameObject = _resourceManager.LoadAsset<GameObject>($"{source.Type}Positive");
        var prefab = gameObject.GetComponent<PositiveItem>();
        var transform = source.transform;
        var item = _objectPool.Get(prefab, Vector3.zero, Quaternion.identity, transform, false);

        return item;
    }

    public NegativeItem GetNegativeItem(NegativeSource source)
    {
        var gameObject = _resourceManager.LoadAsset<GameObject>($"{source.Type}Negative");
        var prefab = gameObject.GetComponent<NegativeItem>();
        var transform = source.transform;
        var item = _objectPool.Get(prefab, Vector3.zero, Quaternion.identity, transform, false);

        return item;
    }

    private void PreparePickupItemsWithExclude<T>(string keyPostfix, params T[] exclude) where T : Enum
    {
        var enums = Enum.GetValues(typeof(T)).Cast<T>().Except(exclude);

        foreach (var value in enums)
        {
            var gameObjectPrefab = _resourceManager.LoadAsset<GameObject>($"{value}{keyPostfix}");
            var prefab = gameObjectPrefab.GetComponent<PickupBase>();
            _objectPool.Prepare(prefab, 4);
        }
    }
}
}