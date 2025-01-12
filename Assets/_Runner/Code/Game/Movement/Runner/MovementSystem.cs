using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runner.Movements
{
public class MoveSetup
{
    public IMovable behaviour;
    public bool isPause;
    public float speed;
}

public class MovementSystem
{
    public bool IsPaused;
    private List<MoveSetup> _movables = new();

    public void Update()
    {
        if (IsPaused)
            return;
        if (_movables.Count == 0)
            return;
        foreach (var setup in _movables)
        {
            if (setup.isPause)
                continue;
            setup.behaviour.Move(setup.speed);
        }
    }

    public MovementSystem AddMovable(MoveSetup setup)
    {
        _movables.Add(setup);

        return this;
    }

    public bool RemoveMovable(MoveSetup setup) => _movables.Remove(setup);
    public bool HasMovable(MoveSetup setup) => _movables.Contains(setup);
    public void Clear() => _movables = new();
    public MoveSetup this[Type type] => _movables.FirstOrDefault(x => x.behaviour.GetType() == type);
}
}