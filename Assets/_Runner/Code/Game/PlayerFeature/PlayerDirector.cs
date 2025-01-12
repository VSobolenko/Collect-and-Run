using System;
using Game.AssetContent;
using Game.Inputs;
using R3;
using Runner.Levels;
using Runner.PlayerFeature.Configs;
using Runner.PlayerFeature.View;
using UnityEngine;
using VContainer.Unity;

namespace Runner.PlayerFeature
{
public class PlayerDirector : ScriptableObjectProviderByAddress<PlayerConfig>, ITickable
{
    public event Action OnUpdateAlive;
    public event Action OnLooser;

    private const string PlayerConfigKey = "Player/Config";

    private readonly PlayerFactory _playerFactory;
    private readonly PlayerConfig _playerConfig;
    private PlayerMoveComponent _moveComponent;
    private Player _player;
    private IDisposable _observeCollector;

    public Player Player => _player;

    public PlayerDirector(IResourceManager resourceManager, PlayerFactory playerFactory) : base(resourceManager)
    {
        _playerFactory = playerFactory;
        _playerConfig = GetSO(PlayerConfigKey);
    }

    public void CreateDefaultPlayerInPosition(Vector3 position)
    {
        var view = _playerFactory.CreatePlayer();
        var stage = GetConfigByStage(_playerConfig.defaultProgress);
        _player = new Player
        {
            view = view,
            stage = stage,
            model = _playerFactory.GetPlayerModelView(view, stage),
        };
        _player.model.Animation.Idle();
        _player.view.SetPosition(position);
        _player.view.Collector.ResetAlive(_playerConfig.defaultProgress);
        _moveComponent = new PlayerMoveComponent(_player.view.Driver, _playerConfig);

        _observeCollector?.Dispose();
        _observeCollector = _player.view.Collector.alive.Subscribe(AliveUpdated);
    }

    private void AliveUpdated(int count)
    {
        if (count > _playerConfig.maxProgress)
        {
            _player.view.Collector.alive.Value = _playerConfig.maxProgress;

            return;
        }

        var possibleNextStage = GetConfigByStage(count);
        _player.view.Progress.SetProgressBar(count, _playerConfig.maxProgress, possibleNextStage.progressBarColor);
        if (count <= 0)
        {
            ProcessDefeat();

            return;
        }

        if (_player.stage == possibleNextStage)
            return;
        var isIncreaseProgress = possibleNextStage.requireProgress > _player.stage.requireProgress;
        _player.model.Release();
        _player.model = _playerFactory.GetPlayerModelView(_player.view, possibleNextStage);
        _player.stage = possibleNextStage;

        if (isIncreaseProgress)
            _player.model.Animation.Happy(1, 1);
        else
            _player.model.Animation.Sadness(1, 1);
        _player.model.Animation.Walk(1);
        OnUpdateAlive?.Invoke();
    }

    private PlayerStageConfig GetConfigByStage(int progress)
    {
        for (var i = 0; i < _playerConfig.configs.Length; i++)
        {
            var config = _playerConfig[i];

            if (progress < config.requireProgress && i > 0)
                return _playerConfig[i - 1];
        }

        return progress > 0 ? _playerConfig.configs[^1] : _playerConfig[0];
    }

    private void ProcessDefeat()
    {
        OnLooser?.Invoke();
    }

    public void StartMoving(Level level, IAxisDetector axisDetector)
    {
        _moveComponent.StartMoving(level, axisDetector);
        _player.model.Animation.Walk(1);
    }

    public void StopMoving()
    {
        _moveComponent.StopMoving();
        _player.model.Animation.Walk(0);
    }

    public void Defeat()
    {
        _player.model.Animation.DefeatDance();
    }

    public void Victory()
    {
        _player.model.Animation.VictoryDance();
    }

    public void Destroy()
    {
        _player.view.Release();
        _player.model.Release();
    }

    public void Tick()
    {
        _moveComponent?.Update();
    }
}

public class Player
{
    //public int activeProgress;
    public View.PlayerView view;
    public PlayerModelView model;
    public PlayerStageConfig stage;
}
}