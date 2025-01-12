using Game.AssetContent;
using Game.Pools;
using Runner.PlayerFeature.Configs;
using Runner.PlayerFeature.View;
using UnityEngine;

namespace Runner.PlayerFeature
{
public class PlayerFactory : PrefabProviderByAddress<View.PlayerView>
{
    private const string PlayerResourcesKey = "Player";
    private readonly IResourceManager _resourceManager;
    private readonly IObjectPoolManager _objectPool;

    public PlayerFactory(IResourceManager resourceManager, IObjectPoolManager objectPool) : base(resourceManager)
    {
        _resourceManager = resourceManager;
        _objectPool = objectPool;
    }

    public View.PlayerView CreatePlayer()
    {
        var prefab = GetPrefab(PlayerResourcesKey);
        _objectPool.Prepare(prefab, 0);
        var player = _objectPool.Get(prefab);
        player.Driver.HorizontalMotor.localPosition = Vector3.zero;
        player.Driver.RotationMotor.localRotation = Quaternion.identity;

        return player;
    }

    public PlayerModelView GetPlayerModelView(PlayerView playerView, PlayerStageConfig config)
    {
        var prefab = _resourceManager.LoadAsset<GameObject>(config.viewKey);
        var view = prefab.GetComponent<PlayerModelView>();
        _objectPool.Prepare(view, 0);
        view = _objectPool.Get(view, playerView.ViewRoot, false);

        return view;
    }
}
}