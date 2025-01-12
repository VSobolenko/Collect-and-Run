using Dreamteck.Splines;
using UnityEngine;

namespace Runner.Movements
{
public class SplineRoad : IMovable
{
    private readonly SplineFollower _follower;

    public SplineRoad(SplineFollower follower)
    {
        _follower = follower;
    }

    public void Move(float speed) => _follower.Move(speed * Time.deltaTime);
}
}