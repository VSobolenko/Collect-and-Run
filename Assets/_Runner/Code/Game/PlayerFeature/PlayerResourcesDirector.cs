using Game.Repositories;
using R3;
using Runner.PlayerFeature.Configs;

namespace Runner.PlayerFeature
{
public class PlayerResourcesDirector
{
    private readonly IRepository<PlayerData> _repository;
    private readonly ReactiveProperty<PlayerData> _data;

    private const int UserDataId = 0;
    public ReadOnlyReactiveProperty<PlayerData> Data => _data;

    public PlayerResourcesDirector(IRepository<PlayerData> repository)
    {
        _repository = repository;
        _data = new ReactiveProperty<PlayerData>
        {
            Value = _repository.Read(UserDataId) ?? InitializeNewPlayerData(),
        };
    }

    private PlayerData InitializeNewPlayerData()
    {
        var data = new PlayerData
        {
            Id = UserDataId,
            ActiveLevelId = 0,
            Money = 50,
        };
        _repository.Create(data);

        return data;
    }
}
}