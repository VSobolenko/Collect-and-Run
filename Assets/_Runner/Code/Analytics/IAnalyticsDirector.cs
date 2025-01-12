using Game;
using Game.Shops;

namespace Runner.Analytics
{
public interface IAnalyticsDirector
{
    void LevelStart(int levelId);
    void LevelVictory(int levelId);
    void LevelDefeat(int levelId);
    void PurchaseResponse(PurchaseResponseResult result, GameProduct gameProduct);
}

public class AnalyticsStub : IAnalyticsDirector
{
    public AnalyticsStub()
    {
        Log.enableAnalyticsEvents = false;
    }

    public void LevelStart(int levelId) => Log.Analytics($"LevelStart: {levelId}");

    public void LevelVictory(int levelId) => Log.Analytics($"LevelVictory: {levelId}");

    public void LevelDefeat(int levelId) => Log.Analytics($"LevelDefeat: {levelId}");

    public void PurchaseResponse(PurchaseResponseResult result, GameProduct gameProduct) =>
        Log.Analytics($"Purchase: Result:{result.result}; Message:{result.message}");
}
}