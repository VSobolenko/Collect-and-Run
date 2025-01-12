using Game.AssetContent;
using Game.Factories;
using UnityEngine;

namespace Runner.Levels
{
public class LevelsFactory
{
    private readonly IResourceManager _resource;
    private readonly IFactoryGameObjects _factory;
    private readonly int _levelsCount;
    private const string LevePrefix = "Road/Level";

    public LevelsFactory(IResourceManager resource, IFactoryGameObjects factory, int levelsCount)
    {
        _resource = resource;
        _factory = factory;
        _levelsCount = levelsCount;
    }

    public Level InstantiateLevel(int id)
    {
        id %= _levelsCount;
        var gameObject = _resource.LoadAsset<GameObject>(LevePrefix + id);
        var prefab = gameObject.GetComponent<Level>();
        var level = _factory.Instantiate(prefab);

        return level;
    }

    public void DestroyLevel(Level level)
    {
        Object.Destroy(level.gameObject);
    }
}
}