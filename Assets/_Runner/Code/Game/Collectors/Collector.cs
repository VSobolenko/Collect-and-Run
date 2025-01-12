using R3;
using UnityEngine;

namespace Runner.Collectors
{
public class Collector : MonoBehaviour
{
    public ReactiveProperty<int> alive = new();

    public void AddPositive(int count)
    {
        alive.Value += count;
    }

    public void AddNegative(int count)
    {
        alive.Value -= count;
    }

    public void ResetAlive(int to) => alive = new ReactiveProperty<int>(to);
}
}