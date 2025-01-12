using Game.Extensions;
using NaughtyAttributes;
using Runner.Collectors.Items.Negative;
using Runner.Collectors.Items.Positive;
using UnityEngine;

namespace Runner
{
public class ItemRandomer : MonoBehaviour
{
    [Button()]
    public void Random()
    {
        var pos = new PositiveType[]
        {
            PositiveType.ManekiNeko,
            PositiveType.Dragon,
            PositiveType.CrystalTree,
            PositiveType.Nekomata,
            PositiveType.Sushi,
        };

        var neg = new NegativeType[]
        {
            NegativeType.Katana,
            NegativeType.DeathNote,
            NegativeType.OniMaskVar1,
            NegativeType.OniMaskVar2,
        };

        var sourcePositive = GetComponentsInChildren<PositiveSource>(true);
        var sourceNegative = GetComponentsInChildren<NegativeSource>(true);
        foreach (var source in sourceNegative)
            source._type = neg.Random();
        foreach (var source in sourcePositive)
            source._type = pos.Random();
    }
}
}