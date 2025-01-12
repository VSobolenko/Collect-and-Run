using Game.Pools;
using Runner.Collectors;
using Runner.Movements;
using Runner.PlayerFeature.WorldCanvas;
using UnityEngine;

namespace Runner.PlayerFeature.View
{
public class PlayerView : MonoPooledObject
{
    [SerializeField] private Driver _driver;
    [SerializeField] private Collector _collector;
    [SerializeField] private Transform _viewRoot;
    [SerializeField] private ProgressBar _progressBar;

    public Driver Driver => _driver;
    public Collector Collector => _collector;
    public Transform ViewRoot => _viewRoot;
    public ProgressBar Progress => _progressBar;

    public void SetPosition(Vector3 position) => transform.position = position;
}
}