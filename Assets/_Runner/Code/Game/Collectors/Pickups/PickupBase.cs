using Game.Pools;

namespace Runner.Collectors.Pickups
{
public class PickupBase : MonoPooledObject
{
    protected override void ValidatePoolKey(ref string poolKey)
    {
        if (string.IsNullOrEmpty(poolKey))
            poolKey = GetType().Name + "." + gameObject.name;
    }
}
}