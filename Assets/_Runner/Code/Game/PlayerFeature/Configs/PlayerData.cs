using System;
using Game.Repositories;

namespace Runner.PlayerFeature.Configs
{
[Serializable]
public class PlayerData : IHasBasicId
{
    public int Id { get; set; }
    public int ActiveLevelId { get; set; }
    public int Money { get; set; }
}
}