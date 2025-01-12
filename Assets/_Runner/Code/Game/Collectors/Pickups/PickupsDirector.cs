using Runner.Collectors.Items.Negative.Types;
using Runner.Collectors.Items.Positive;
using Runner.Levels;

namespace Runner.Collectors
{
public class PickupsDirector
{
    private readonly PickupFactory _factory;

    public PickupsDirector(PickupFactory factory)
    {
        _factory = factory;
    }

    public PositiveItem[] SetupPositive(Level level)
    {
        var items = new PositiveItem[level.positiveItems.Length];
        for (var i = 0; i < level.positiveItems.Length; i++)
        {
            var source = level.positiveItems[i];
            var item = _factory.GetPositiveItem(source);
            items[i] = item;
        }

        return items;
    }

    public NegativeItem[] SetupNegative(Level level)
    {
        var items = new NegativeItem[level.negativeSources.Length];
        for (var i = 0; i < level.negativeSources.Length; i++)
        {
            var source = level.negativeSources[i];
            var item = _factory.GetNegativeItem(source);
            items[i] = item;
        }

        return items;
    }
}
}